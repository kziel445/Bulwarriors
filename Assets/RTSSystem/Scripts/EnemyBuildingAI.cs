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
    public int maxWorkers = 6;
    public bool advancedBuilderAvailable = false;
    public int maxAdvancedWorkers = 2;
    public float goldModifier = 2;

    public List<Interactable> buildings = new List<Interactable>();
    List<Vector2> targetPositionList;
    public Transform parentBuildings;
    public Transform parentUnits;
    
    void Start()
    {
        try
        {
            stats = GameObject.Find("EnemyStatistics").GetComponent<Statistics.Statistics>();
            stats.goldModifier = goldModifier;
        }
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
        float difference = stats.money - (moneyForBuildings + moneyForUnits);
        if(GameObject.Find("EnemyData").GetComponent<Statistics.Data>().timer<30)
        {
            moneyForBuildings += difference;
        }
        else if(stats.money > moneyForBuildings + moneyForUnits)
        {
            moneyForBuildings += difference * 0.6f;
            moneyForUnits += difference * 0.4f;
        }
        else if (GameObject.Find("EnemyData").GetComponent<Statistics.Data>().timer > 180)
        {
            moneyForBuildings += difference * 0.2f;
            moneyForUnits += difference * 0.8f;
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
                if (parentBuildings.Find("Houses").childCount >= 1 && GameObject.Find("EnemyData").GetComponent<Statistics.Data>().timer < 40)
                {
                    buildingNameToBuild = "Barrack";
                }
                else if (GameObject.Find("EnemyData").GetComponent<Statistics.Data>().timer < 90)
                {
                    buildingNameToBuild = "House";
                }
                else
                {
                    buildingNameToBuild = availableBuildings[Random.Range(0, availableBuildings.Count)].name;
                }
            }
            var buildingCost = GameObject.Find("EnemyBuilderManager").GetComponent<Buildings.Builder>().IsBuilding(buildingNameToBuild).baseStats.cost;
            if(GameObject.Find("EnemyData").GetComponent<Statistics.Data>().timer < 60)
                yield return new WaitUntil(() => moneyForBuildings >= buildingCost);
            else
                yield return new WaitForSeconds(4);
            if(moneyForBuildings >= buildingCost)
            {
                moneyForBuildings -= buildingCost;
                var positionFree = false;
                Vector2 position = new Vector2(0,0);
                int positionIndex = 1;
                while(!positionFree)
                {
                    if (buildingNameToBuild == "Citadel")
                    {
                        position = targetPositionList[0];
                        positionFree = true;
                    }
                    else
                    {
                        position = targetPositionList[positionIndex];
                        positionFree = GameObject.Find("EnemyBuilderManager").GetComponent<Buildings.Builder>().CheckIfFreeSpace(position, new Vector2(1, 1));
                        positionIndex++;
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
            var building = buildings[Random.Range(0,buildings.Count)].gameObject;
            Debug.Log(building);
            //avaialbeUnits in building
            var availableUnits = building.GetComponent<Buildings.BuildingRTS>().baseStats.actions.basicUnits;
            //get name of unit from list
            string unitNameToRecruit;
            bool passUnit = false;

                unitNameToRecruit = availableUnits[Random.Range(0, availableUnits.Count)].name;

            if (unitNameToRecruit == "Worker" && parentUnits.transform.Find("Workers").childCount < maxWorkers)
                passUnit = true;
            else if (unitNameToRecruit == "AdvancedWorker"
                && parentUnits.transform.Find("AdvancedWorkers").childCount < maxAdvancedWorkers
                && GameObject.Find("EnemyData").GetComponent<Statistics.Data>().timer > 300)
                passUnit = true;
            else if(unitNameToRecruit == "Scout" && parentUnits.transform.Find("Workers").childCount < 1)
            {
                passUnit = true;
            }
            else if(unitNameToRecruit != "Scout" && unitNameToRecruit != "Worker" && unitNameToRecruit != "AdvancedWorker")
                passUnit = true;
            Debug.Log(building.GetComponent<Buildings.ObjectSpawnQueue>());
            if(passUnit)
            {
                Debug.Log(unitNameToRecruit + " recruting");
                var unitReturn = building.GetComponent<Buildings.ObjectSpawnQueue>()
                    .IsUnit(unitNameToRecruit);
                var unitCost = unitReturn.baseStats.cost;

                if (GameObject.Find("EnemyData").GetComponent<Statistics.Data>().timer > 300)
                    yield return new WaitUntil(() => moneyForUnits >= unitCost);
                else
                    yield return new WaitForSeconds(2);
                if (moneyForUnits >= unitCost)
                {
                    moneyForUnits -= unitCost;
                    building.GetComponent<Buildings.ObjectSpawnQueue>()
                        .StartQueueTimer(unitNameToRecruit);
                }
            }
        }
        yield return new WaitForSeconds(1);
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
