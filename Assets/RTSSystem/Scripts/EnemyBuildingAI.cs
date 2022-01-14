using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Interactables;

public class EnemyBuildingAI : MonoBehaviour
{
    public Statistics.Statistics stats;
    public float moneyForUnits = 0;
    public float moneyForBuildings = 0;
    public bool builderAvailable = true;
    public int maxWorkers = 8;
    public bool advancedBuilderAvailable = false;
    public int maxAdvancedWorkers = 2;

    public List<Interactable> buildings = new List<Interactable>();
    List<Vector2> targetPositionList;
    public Transform parentBuildings;
    public Transform parentUnits;
    
    void Start()
    {
        try {stats = GameObject.Find("EnemyStatistics").GetComponent<Statistics.Statistics>();}
        catch {Debug.LogWarning("Cant find enemy statistics");}
        try {parentBuildings = GameObject.Find("EnemyBuildings").transform;}
        catch {Debug.LogWarning("Cant find enemy builidings");}
        try {parentUnits = GameObject.Find("EnemyUnits").transform;}
        catch {Debug.LogWarning("Cant find enemy units");}
        StartCoroutine(RecrutNewUnit());
        StartCoroutine(BuildNewStructure());
        AddStructurePositions();
    }
    void Update()
    {
        //control segment
        AllocateGold();
        //check if new buildings available
        
        CheckIfBuildersAvaiable();
    }
    public void AllocateGold()
    {
        if(GameObject.Find("EnemyData").GetComponent<Statistics.Data>().timer<30)
        {
            float difference = stats.money - (moneyForBuildings + moneyForUnits);
            moneyForBuildings += difference;
        }
        if(stats.money > moneyForBuildings + moneyForUnits)
        {
            float difference = stats.money - (moneyForBuildings + moneyForUnits);
            moneyForBuildings += difference * 0.5f;
            moneyForUnits += difference * 0.5f;
        }
    }
    public void CheckIfNewRecrutationBuildings()
    {
        foreach(Transform group in parentBuildings)
        {
            foreach(Transform building in group)
            {
                var interactables = building.GetComponent<Interactable>();
                if(!buildings.Contains(interactables) 
                    && building.GetComponent<Buildings.Enemy.EnemyBuilding>().isBuilded)
                {
                    if(building.GetComponent<Buildings.ObjectSpawnQueue>()) buildings.Add(interactables);
                }
            }
        }
    }
    public void CheckIfBuildersAvaiable()
    {
        if(parentUnits.Find("AdvancedWorkers").childCount>0)
        {
            advancedBuilderAvailable = true;
        }
        else advancedBuilderAvailable = false;

        if(parentUnits.Find("Workers").childCount>0)
        {
            builderAvailable = true;
        }
        else builderAvailable = false;
    }
    public IEnumerator BuildNewStructure()
    {

        if(builderAvailable || advancedBuilderAvailable)
        {
            string buildingNameToBuild;
            Transform unit = null;
            if(advancedBuilderAvailable)
            {
                unit = parentUnits.transform.Find("AdvancedWorkers").GetChild(0);
                var availableBuildings = unit.GetComponent<Units.UnitRTS>().baseStats.actions.basicBuildings;
                
                if(parentBuildings.transform.Find("Citadels").childCount==0) 
                {
                    buildingNameToBuild = "Citadel";
                }
                else
                {
                    do{
                        buildingNameToBuild = availableBuildings[Random.Range(0, availableBuildings.Count)].name;
                    }while(buildingNameToBuild != "Citadel");
                }
            }
            else
            {
                unit = parentUnits.transform.Find("Workers").GetChild(0);
                var availableBuildings = unit.GetComponent<Units.UnitRTS>().baseStats.actions.basicBuildings;
                if(GameObject.Find("EnemyData").GetComponent<Statistics.Data>().timer < 40)
                {
                    buildingNameToBuild = "House";
                }
                else
                {
                    buildingNameToBuild = availableBuildings[Random.Range(0, availableBuildings.Count)].name;

                }

            }
            var buildingCost = GameObject.Find("EnemyBuilderManager").GetComponent<Buildings.Builder>().IsBuilding(buildingNameToBuild).baseStats.cost;

            yield return new WaitUntil(() => moneyForBuildings >= buildingCost);
            if(moneyForBuildings >= buildingCost)
            {
                moneyForBuildings -= buildingCost;
                var positionFree = false;
                Vector2 position = new Vector2(0,0);
                while(!positionFree)
                {
                    if (buildingNameToBuild == "Citadel")
                    {
                        position = targetPositionList[0];
                        positionFree = true;
                    }
                    else
                    {
                        position = targetPositionList[Random.Range(1, targetPositionList.Count)];
                        positionFree = GameObject.Find("EnemyBuilderManager").GetComponent<Buildings.Builder>().CheckIfFreeSpace(position, new Vector2(1, 1));
                    }
                }
                GameObject.Find("EnemyBuilderManager").GetComponent<Buildings.Builder>().SpawnNewBuilding(position, buildingNameToBuild);
                gameObject.GetComponent<EnemyUnitAI>().SendWorkersToBuild(position);
                var worker = parentUnits.Find("Workers").GetChild(0).GetComponent<Units.WorkerFunctions>();
                yield return new WaitUntil(() => worker.isRepairing == false);
            }
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(BuildNewStructure());
    }
    public IEnumerator RecrutNewUnit()
    {
        CheckIfNewRecrutationBuildings();
        if (buildings.Count != 0)
        {
            
            //random building from existing
            var building = buildings[Random.Range(0,buildings.Count)];
            //avaialbeUnits in building
            var availableUnits = building.GetComponent<Buildings.BuildingRTS>().baseStats.actions.basicUnits;
            //get name of unit from list
            string unitNameToRecruit;
            bool passUnit = false;
            do
            {
                unitNameToRecruit = availableUnits[Random.Range(0, availableUnits.Count)].name;

                if (unitNameToRecruit == "Worker" && parentUnits.transform.Find("Workers").childCount < maxWorkers)
                    passUnit = true;
                else if (unitNameToRecruit == "AdvancedWorker"
                    && parentUnits.transform.Find("AdvancedWorkers").childCount < maxAdvancedWorkers
                    && GameObject.Find("EnemyData").GetComponent<Statistics.Data>().timer > 300)
                    passUnit = true;
                else passUnit = true;
            } while (!passUnit);
              
            var unitCost = building.GetComponent<Buildings.ObjectSpawnQueue>()
                .IsUnit(unitNameToRecruit).baseStats.cost;
            yield return new WaitUntil(() => moneyForUnits >= unitCost);
            if(moneyForUnits >= unitCost)
            {
                moneyForUnits -= unitCost;
                building.GetComponent<Buildings.ObjectSpawnQueue>()
                    .StartQueueTimer(unitNameToRecruit);
            }
        }
        yield return new WaitForSeconds(2);
        StartCoroutine(RecrutNewUnit());
    }
    public void AddStructurePositions()
    {
        targetPositionList = GetPositionListAround(parentBuildings.position, new float[] { 3, 6, 9 }, new int[] { 8, 16, 24 });        
    }
    public List<Vector2> GetPositionListAround(Vector2 startPosition, float[] ringDistanceArray, int[] ringPositionCountArray)
    {
        List<Vector2> positionList = new List<Vector2>();
        positionList.Add(startPosition);
        for (int i = 0; i < ringDistanceArray.Length; i++)
        {
            positionList.AddRange(GetPositionListAround(startPosition, ringDistanceArray[i], ringPositionCountArray[i]));
        }
        return positionList;
    }
    private List<Vector2> GetPositionListAround(Vector2 startPosition, float distance, int positionCount)
    {
        List<Vector2> positionList = new List<Vector2>();
        for (int i = 0; i < positionCount; i++)
        {
            float angle = i * (360f / positionCount);
            Vector2 dir = ApplyRotationToVector(new Vector2(1, 0), angle);
            Vector2 position = startPosition + dir * distance;
            positionList.Add(position);
        }
        return positionList;
    }    
        private Vector2 ApplyRotationToVector(Vector2 vec, float angle)
        {
            return Quaternion.Euler(0, 0, angle) * vec;
        }
}
