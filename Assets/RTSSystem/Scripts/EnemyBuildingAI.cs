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
    public bool advancedBuilderAvailable = false;

    public List<Interactable> buildings = new List<Interactable>();
    List<Vector2> targetPositionList;
    public Transform parentBuildings;
    public Transform parentUnits;
    //tmp 
    public GameObject prefab;
    
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        //control segment
        AllocateGold();
        //check if new buildings available
        CheckIfNewRecrutationBuildings();
        CheckIfBuildersAvaiable();
    }
    public void AllocateGold()
    {
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
                if(building.GetComponent<Buildings.ObjectSpawnQueue>() && !buildings.Contains(interactables))
                {
                    buildings.Add(interactables);
                }
            }
        }
    }
    public void CheckIfBuildersAvaiable()
    {
        foreach(Transform group in parentUnits)
        {
            if(group.name.Contains("AdvancedWorkers"))
            {
                if(group.childCount>0) advancedBuilderAvailable = true;
            }
            else advancedBuilderAvailable = false;

            if(group.name.Contains("Workers"))
            {
                if(group.childCount>0) builderAvailable = true;
            }
            else builderAvailable = false;
        }
    }
    public IEnumerator BuildNewStructure()
    {
        
        if(builderAvailable || advancedBuilderAvailable)
        {
            Debug.Log("Builder available");
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
                    buildingNameToBuild = availableBuildings[Random.Range(0,availableBuildings.Count)].name;
                }
            }
            else
            {
                unit = parentUnits.transform.Find("Workers").GetChild(0);
                var availableBuildings = unit.GetComponent<Units.UnitRTS>().baseStats.actions.basicBuildings;
                buildingNameToBuild = availableBuildings[Random.Range(0,availableBuildings.Count)].name;
            }
            var buildingCost = GameObject.Find("EnemyBuilderManager").GetComponent<Buildings.Builder>()
                .IsBuilding(buildingNameToBuild).baseStats.cost;

            Debug.Log("Wait to build: "+ buildingNameToBuild);
            yield return new WaitUntil(() => moneyForBuildings >= buildingCost);
            if(moneyForBuildings >= buildingCost)
            {
                moneyForBuildings -= buildingCost;
                Debug.Log("Building: " + buildingNameToBuild);
                GameObject.Find("EnemyBuilderManager")
                    .GetComponent<Buildings.Builder>().SpawnNewBuilding(new Vector2(0,0),buildingNameToBuild);

                //build in position closest to center and check is position free or wait until its free
            }
            

        }
        
        yield return new WaitForSeconds(1);
        StartCoroutine(BuildNewStructure());
    }
    public IEnumerator RecrutNewUnit()
    {
        if(buildings.Count != 0)
        {
            //random building from existing
            var building = buildings[Random.Range(0,buildings.Count)];
            //avaialbeUnits in building
            var availableUnits = building.GetComponent<Buildings.BuildingRTS>().baseStats.actions.basicUnits;
            //get name of unit from list
            var unitNameToRecruit = availableUnits[Random.Range(0,availableUnits.Count)];
            //Units.UnitBasic unit = IsUnit(objectToSpawn);
            //statistics.money -= unit.baseStats.cost;
            var unitCost = building.GetComponent<Buildings.ObjectSpawnQueue>()
                .IsUnit(unitNameToRecruit.name.ToString()).baseStats.cost;
            yield return new WaitUntil(() => moneyForUnits >= unitCost);
            if(moneyForUnits >= unitCost)
            {
                moneyForUnits -= unitCost;
                building.GetComponent<Buildings.ObjectSpawnQueue>()
                    .StartQueueTimer(unitNameToRecruit.name.ToString());
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
