using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cursor;
using Units;

// script for selection units and give commands
public class InputHandler : MonoBehaviour
{
    [SerializeField] private Transform selectionAreaTransform;
    private Vector2 startPosition;
    private List<UnitRTS> selectedUnitRTSList;
    private Position cursorPosition = new Position();
    public Transform playerUnits;

    [SerializeField] private Camera camera;

    private void Awake()
    {
        selectedUnitRTSList = new List<UnitRTS>();
        selectionAreaTransform.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            // get cursor cordinates, when LPM state is changed
            startPosition = cursorPosition.getMousePosition();
            selectionAreaTransform.gameObject.SetActive(true);
        }
        if (Input.GetMouseButton(0))
        {
            // create field to select units
            Vector2 currentMousePosition = cursorPosition.getMousePosition();
            Vector2 lowerLeft = new Vector2(
                Mathf.Min(startPosition.x, currentMousePosition.x),
                Mathf.Min(startPosition.y, currentMousePosition.y)
                );
            Vector2 upperRight = new Vector2(
                Mathf.Max(startPosition.x, currentMousePosition.x),
                Mathf.Max(startPosition.y, currentMousePosition.y)
                );
            selectionAreaTransform.position = lowerLeft;
            selectionAreaTransform.localScale = upperRight - lowerLeft;
        }
        if (Input.GetMouseButtonUp(0))
        {
            // get curosor cordinates, when LPM state is changed, again
            selectionAreaTransform.gameObject.SetActive(false);
            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosition, cursorPosition.getMousePosition());
            // deslect units

            foreach (UnitRTS unitRTS in selectedUnitRTSList)
            {
                unitRTS.SetSelectedVisible(false);
            }
            selectedUnitRTSList.Clear();
            // select units
            foreach (Collider2D collider2D in collider2DArray)
            {
                UnitRTS unitRTS = collider2D.GetComponent<UnitRTS>();
                if (unitRTS != null && unitRTS.gameObject.layer==8)
                {
                    unitRTS.SetSelectedVisible(true);
                    selectedUnitRTSList.Add(unitRTS);
                }
            }
            // show how manyh objects has been selected
            Debug.Log(selectedUnitRTSList.Count);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 moveToPosition = cursorPosition.getMousePosition();
            // TODO: set dynamic vlaues down below
            List<Vector2> targetPositionList = GetPositionListAround(moveToPosition, new float[] {0.5f,1,1.5f}, new int[] { 5, 10, 20 });
            int targetPositionListIndex = 0;
            foreach (UnitRTS unitRTS in selectedUnitRTSList)
            {
                unitRTS.MoveTo(targetPositionList[targetPositionListIndex]);
                targetPositionListIndex = (targetPositionListIndex + 1) % targetPositionList.Count;
            }
        }
        // attacking
        if (Input.GetMouseButtonDown(1) && selectedUnitRTSList.Count!=0)
        {
            Vector2 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D clicked = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (clicked)
            {
                IClickable clickable = clicked.collider.GetComponent<IClickable>();
                int unitLayer = selectedUnitRTSList[0].Layer();
                if(clickable.Layer() != unitLayer && clickable.Layer() != unitLayer+1)
                {
                    Debug.Log(selectedUnitRTSList[0].name + " group is going to attack " + clickable);
                    foreach (UnitRTS unitRTS in selectedUnitRTSList)
                    {
                        unitRTS.attackObjective = clickable;
                    }
                    Debug.Log("Attacking objective is " + selectedUnitRTSList[0].attackObjective);
                }


            }
        }



        // when I-key pressed, select all units
        if (Input.GetKey(KeyCode.I))
        {
            SelectAllUnits();
            Debug.Log(selectedUnitRTSList.Count);
        }

        RaycastHit2D hit = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if(hit.collider!= null)
        {
            
            Debug.Log(hit.collider.gameObject.tag);
        }
    }
    private void SelectAllUnits()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(new Vector2(-100, -100), new Vector2(1000, 1000));

        //deslect units
        foreach (UnitRTS unitRTS in selectedUnitRTSList)
        {
            unitRTS.SetSelectedVisible(false);
        }
        selectedUnitRTSList.Clear();
        //select units
        foreach (Collider2D collider2D in collider2DArray)
        {
            UnitRTS unitRTS = collider2D.GetComponent<UnitRTS>();
            if (unitRTS != null)
            {
                unitRTS.SetSelectedVisible(true);
                selectedUnitRTSList.Add(unitRTS);
            }
        }
    }
    private List<Vector2> GetPositionListAround(Vector2 startPosition, float[] ringDistanceArray, int[] ringPositionCountArray)
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
        for(int i = 0; i < positionCount; i++)
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
