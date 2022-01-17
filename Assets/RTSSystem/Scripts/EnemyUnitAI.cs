using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Interactables;

public class EnemyUnitAI : MonoBehaviour
{
    [SerializeField] Transform parentOfUnits;
    public List<Interactable> selectedUnits;
    public List<Interactable> unitsAvailable;

    public delegate void CommandMethod();
    private bool waitingForCommand = true;
    public Transform playerBuildingsParent;
    [SerializeField] internal Vector2 groupPoint;

    private void Awake()
    {
        if (parentOfUnits == null)
        {
            parentOfUnits = GameObject.Find("EnemyUnits").transform;
        }
        else Debug.LogWarning("EnemyUnits parent not found");
        playerBuildingsParent = GameObject.Find("PlayerBuildings").transform;
    }

    void Update()
    {
        if(waitingForCommand)
        {
            waitingForCommand = false;
            StartCoroutine(Command(60, ChooseRandomUnits));
        }

    }

    public void ChooseRandomUnits(float percentOfUnit = 0f)
    {
        if (percentOfUnit == 0) percentOfUnit = Random.RandomRange(10f, 50f);
        TakeUnitsWithParameters(percentOfUnit);
    }

    public void ChooseRandomUnits()
    {
        float percentOfUnit = Random.RandomRange(10f, 50f);
        TakeUnitsWithParameters(percentOfUnit);
    }

    private void TakeUnitsWithParameters(float percentOfUnit)
    {
        int unitsCount = 0;
        foreach (Transform group in parentOfUnits)
        {
            if (!group.name.Contains("Workers"))
            {
                foreach (Transform unit in group)
                {
                    if (!unit.GetComponent<Units.UnitRTS>().IfCommand)
                    {
                        if(!unitsAvailable.Contains(unit.GetComponent<Interactable>()))
                            unitsAvailable.Add(unit.GetComponent<Interactable>());
                        unitsCount++;
                    }
                }
            }
        }
        unitsCount = (int)(unitsCount * percentOfUnit/100);
        while (unitsCount != 0)
        {
            var unit = unitsAvailable[Random.Range(0, unitsAvailable.Count - 1)];
            selectedUnits.Add(unit);
            unitsAvailable.Remove(unit);
            unitsCount--;
        }
    }

    public void ChooseAllUnits()
    {
        foreach (Transform group in parentOfUnits)
        {
            foreach (Transform unit in group)
            {
                selectedUnits.Add(unit.transform.GetComponent<Interactable>());
            }
        }
    }

    public void GroupMove(Vector2 moveToPosition, List<Interactable> selectedUnit = null)
    {
        if (selectedUnit == null) selectedUnit = selectedUnits;
        List<Vector2> targetPositionList = GetPositionListAround(moveToPosition, new float[] { 0.5f, 1, 1.5f,2f}, new int[] { 5, 10, 20, 40});
        int targetPositionListIndex = 0;
        foreach (Interactable interactableObject in selectedUnit)
        {
            Units.UnitRTS unitRTS = interactableObject.GetComponent<Units.UnitRTS>();
            unitRTS.MoveTo(targetPositionList[targetPositionListIndex]);
            unitRTS.IfCommand = true;
            targetPositionListIndex = (targetPositionListIndex + 1) % targetPositionList.Count;
        }
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

    public IEnumerator Command(float time, CommandMethod method)
    {
        method();
        yield return new WaitForSeconds(time);
        GroupMove(groupPoint);
        Debug.Log("Command formation");
        yield return new WaitForSeconds(3);
        Debug.Log("Command start");
        if(playerBuildingsParent.Find("Citadels").childCount>0)
        {
            GroupMove(playerBuildingsParent.Find("Citadels").GetChild(0).gameObject.transform.position);
        }
        else
        {
            List<int> buildingsCounts = new List<int>();
            foreach(Transform group in playerBuildingsParent)
            {
                buildingsCounts.Add(group.childCount);
            }
            int groupNumber;
            do
            {
                groupNumber = Random.Range(0, buildingsCounts.Count - 1);
            } while (buildingsCounts[groupNumber] == 0);
            
            var buildingNumber = Random.Range(0, buildingsCounts[groupNumber]-1);
            GroupMove(playerBuildingsParent.GetChild(groupNumber)
                .GetChild(buildingNumber).gameObject.transform.position);
        }
        
        selectedUnits.Clear();
        waitingForCommand = true;
    }

    public void SendWorkersToBuild(Vector2 target)
    {
        foreach (Transform group in parentOfUnits)
        {   
            if(group.name.Contains("Workers"))
            {
                foreach(Transform unit in group)
                {
                    unit.GetComponent<Units.UnitRTS>().MoveTo(target);
                    if (unit.GetComponent<Units.WorkerFunctions>() == null)
                    {
                        unit.gameObject.AddComponent<Units.WorkerFunctions>();
                    }
                    RaycastHit2D hit = Physics2D.Raycast(target, Vector2.up);
                    unit.GetComponent<Units.WorkerFunctions>().SetRepairValues(true, hit.collider.gameObject);
                }
            }
        }
    }
    
    public void CheckForUndoneBuildings()
    {
        var parentBuilding = gameObject.GetComponent<EnemyBuildingAI>().parentBuildings;
        foreach(Transform group in parentBuilding)
        {
            
        }
    }
}
