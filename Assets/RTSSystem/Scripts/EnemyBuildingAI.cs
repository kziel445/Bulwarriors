using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Interactables;

public class EnemyBuildingAI : MonoBehaviour
{
    public Statistics.Statistics stats;
    public float moneyForUnits;
    public float moneyForBuildings;

    public List<Interactable> buildings;
    public Transform parentBuildings;
    
    // Start is called before the first frame update
    void Start()
    {
        try {stats = GameObject.Find("EnemyStatistics").GetComponent<Statistics.Statistics>();}
        catch {Debug.LogWarning("Cant find enemy statistics");}
        try {parentBuildings = GameObject.Find("EnemyBuildings").transform;}
        catch {Debug.LogWarning("Cant find enemy builidings");}
    }

    // Update is called once per frame
    void Update()
    {
        //control segment
        AllocateGold();
        CheckIfNewRecrutationBuildings();

        //recruting units
        if(moneyForUnits > 100) RecrutNewUnit();
        

    }
    public void AllocateGold()
    {
        if(stats.money > 100)
        {
            moneyForBuildings += stats.money * 0.5f;
            moneyForUnits += stats.money * 0.5f;
            stats.money = 0;
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
    public void RecrutNewUnit()
    {
        var building = buildings[Random.Range(0,buildings.Count)];
        var availableUnits = building.GetComponent<Buildings.BuildingRTS>().baseStats.actions.basicUnits;
        Debug.Log(availableUnits[Random.Range(0,availableUnits.Count)].name);
        building.GetComponent<Buildings.ObjectSpawnQueue>()
            .StartQueueTimer(availableUnits[Random.Range(0,availableUnits.Count)].name.ToString());
        //and so on like i those script

    }

}
