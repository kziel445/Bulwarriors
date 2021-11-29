using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputManager;
using Core.Interactables;

public class EnemyAIHandler : MonoBehaviour
{
    [SerializeField] Transform parentOfUnits;
    public List<Interactable> selectedUnit;
    public List<Interactable> unitsWithCommands;
    [SerializeField] Transform parentOfBuildings;
    public List<Interactable> selectedBuildings;

    public delegate void CommandMethod();
    private bool waitingForCommand = true;
    [SerializeField] private Vector2 playerBase;
    [SerializeField] private Vector2 groupPoint;

    private void Awake()
    {
        if (parentOfUnits == null)
        {
            parentOfUnits = GameObject.Find("EnemyUnits").transform;
        }
        else Debug.LogWarning("EnemyUnits parent not found");

    }
    // Update is called once per frame
    void Update()
    {
        if(waitingForCommand)
        {
            waitingForCommand = false;
            StartCoroutine(Command(10,ChooseRandomUnits));
            Debug.Log("Command get");
            //ChooseRandomUnits();
        }
        if(selectedUnit.Count==0)
        {
            
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            ChooseRandomUnits(1, 50);
            GroupMove(playerBase);
            selectedUnit.Clear();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ChooseRandomUnits(2, 10);
            GroupMove(new Vector2(30, 10));
            selectedUnit.Clear();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChooseRandomUnits(3, 60);
            GroupMove(new Vector2(30, 10));
            selectedUnit.Clear();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            ChooseRandomUnits();
            GroupMove(new Vector2(30, 10));
            selectedUnit.Clear();
        }
    }
    public void ChooseRandomUnits(int option = 0, float percentOfUnit = 0f)
    {
        if(option==0) option = Random.Range(1, 3);
        if (percentOfUnit == 0) percentOfUnit = Random.RandomRange(10f, 50f);
        TakeUnitsWithParameters(option, percentOfUnit);
    }
    public void ChooseRandomUnits()
    {
        int option = Random.Range(1, 3);
        float percentOfUnit = Random.RandomRange(10f, 50f);
        TakeUnitsWithParameters(option, percentOfUnit);

    }
    private void TakeUnitsWithParameters(int option, float percentOfUnit)
    {
        switch (option)
        {
            case 1:
                foreach (Transform unit in parentOfUnits.GetChild(0))
                {   
                    if(!unit.GetComponent<Units.UnitRTS>().IfCommand) selectedUnit.Add(unit.transform.GetComponent<Interactable>());
                    {
                        break;
                    }
                }
                Debug.Log("Selected units " + selectedUnit.Count);
                break;
            case 2:
                foreach (Transform unit in parentOfUnits.GetChild(2))
                {
                    if (!unit.GetComponent<Units.UnitRTS>().IfCommand) selectedUnit.Add(unit.transform.GetComponent<Interactable>());
                    if (parentOfUnits.GetChild(2).childCount * percentOfUnit / 100 <= selectedUnit.Count)
                    {
                        break;
                    }
                }
                Debug.Log("Selected units " + selectedUnit.Count);
                break;
            case 3:
                float percentClass = Random.Range(0f, percentOfUnit);
                foreach (Transform unit in parentOfUnits.GetChild(0))
                {
                    if (!unit.GetComponent<Units.UnitRTS>().IfCommand) selectedUnit.Add(unit.transform.GetComponent<Interactable>());
                    if (parentOfUnits.GetChild(0).childCount * percentOfUnit - percentClass / 100 <= selectedUnit.Count)
                    {
                        break;
                    }
                }
                foreach (Transform unit in parentOfUnits.GetChild(2))
                {
                    if(!unit.GetComponent<Units.UnitRTS>().IfCommand) selectedUnit.Add(unit.transform.GetComponent<Interactable>());
                    if (parentOfUnits.GetChild(2).childCount * percentOfUnit - (percentOfUnit - percentClass) / 100 <= selectedUnit.Count)
                    {
                        break;
                    }
                }
                Debug.Log("Selected units " + selectedUnit.Count);
                break;
        }
    }
    public void ChooseAllUnits()
    {
        foreach (Transform group in parentOfUnits)
        {
            foreach (Transform unit in group)
            {
                selectedUnit.Add(unit.transform.GetComponent<Interactable>());
            }
        }
    }
    public void GroupMove(Vector2 moveToPosition)
    {
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
        yield return new WaitForSeconds(5);
        Debug.Log("Command start");
        GroupMove(playerBase);
        selectedUnit.Clear();
        waitingForCommand = true;

    }
}
