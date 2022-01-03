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

    public List<Interactable> buildings = new List<Interactable>();
    public Transform parentBuildings;
    public Transform parentUnits;
    
    // Start is called before the first frame update
    void Start()
    {
        try {stats = GameObject.Find("EnemyStatistics").GetComponent<Statistics.Statistics>();}
        catch {Debug.LogWarning("Cant find enemy statistics");}
        try {parentBuildings = GameObject.Find("EnemyBuildings").transform;}
        catch {Debug.LogWarning("Cant find enemy builidings");}
        try {parentUnits = GameObject.Find("EnemyBuildings").transform;}
        catch {Debug.LogWarning("Cant find enemy units");}
        StartCoroutine(RecrutNewUnit());
        StartCoroutine(BuildNewStructure());
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
            if(group.name.Contains("Workers"))
            {
                if(group.childCount>0) builderAvailable = true;
                return;
            }
        }
        builderAvailable = false;
    }
    public IEnumerator BuildNewStructure()
    {
        if(builderAvailable)
        {
            //using code down below write buidling script
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(BuildNewStructure());
    }
    public IEnumerator RecrutNewUnit()
    {
        if(buildings.Count != 0)
        {
            var building = buildings[Random.Range(0,buildings.Count)];
            var availableUnits = building.GetComponent<Buildings.BuildingRTS>().baseStats.actions.basicUnits;
            var unitNameToRecruit = availableUnits[Random.Range(0,availableUnits.Count)];
            //Units.UnitBasic unit = IsUnit(objectToSpawn);
            //statistics.money -= unit.baseStats.cost;
            var unitCost = building.GetComponent<Buildings.ObjectSpawnQueue>()
                .IsUnit(unitNameToRecruit.name.ToString()).baseStats.cost;
            yield return new WaitUntil(() => moneyForUnits >= unitCost);
            if(moneyForUnits >= unitCost)
            {
                Debug.Log(moneyForUnits + " " + unitCost);

                moneyForUnits -= unitCost;
                building.GetComponent<Buildings.ObjectSpawnQueue>()
                    .StartQueueTimer(unitNameToRecruit.name.ToString());
            }
                
        }
        yield return new WaitForSeconds(2);
        StartCoroutine(RecrutNewUnit());
        
    }

}
