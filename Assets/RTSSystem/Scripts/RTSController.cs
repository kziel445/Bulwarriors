using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using Cursor;


// script for selection units and give commands
public class RTSController : MonoBehaviour
{
    [SerializeField] private Transform selectionAreaTransform;
    private Vector3 startPosition;
    private List<UnitRTS> selectedUnitRTSList;
    private Position cursorPosition = new Position();
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
            Debug.Log("1" + cursorPosition.getMousePosition());
            // get curosor cordinates, when LPM state is changed
            startPosition = UtilsClass.GetMouseWorldPosition();
            selectionAreaTransform.gameObject.SetActive(true);
        }
        if (Input.GetMouseButton(0))
        {
            // create field to select units
            Vector3 currentMousePosition = UtilsClass.GetMouseWorldPosition();
            Vector3 lowerLeft = new Vector3(
                Mathf.Min(startPosition.x, currentMousePosition.x),
                Mathf.Min(startPosition.y, currentMousePosition.y)
                );
            Vector3 upperRight = new Vector3(
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
            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosition, UtilsClass.GetMouseWorldPosition());
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
                if (unitRTS != null)
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
            Vector3 movePosition = UtilsClass.GetMouseWorldPosition();
            foreach (UnitRTS unitRTS in selectedUnitRTSList)
            {
                unitRTS.MoveTo(movePosition);
            }
        }

        // when I-key pressed, select all units
        if (Input.GetKey(KeyCode.I))
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
            Debug.Log(selectedUnitRTSList.Count);
        }
    }

}
