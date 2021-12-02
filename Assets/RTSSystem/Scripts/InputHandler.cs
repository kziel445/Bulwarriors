using System.Collections.Generic;
using UnityEngine;
using KZ.Cursor;
using Units.Player;
using Core.Interactables;
using UI;
using UnityEngine.EventSystems;

namespace InputManager
{
    // script for selection units and give commands
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler instance;

        [SerializeField] internal Transform selectionAreaTransform;
        private Vector2 startPosition;

        //object selection
        public List<Interactable> selectedUnitRTSList;
        public Transform selectedObject=null;
        public bool isSelectedBuilding = false;
        public bool isSelecting = false;

        //cursor
        private Position cursorPosition = new Position();
        //unused?
        public Transform playerUnits;

        [SerializeField] private Camera camera;

        private void Awake()
        {
            instance = this;
            selectedUnitRTSList = new List<Interactable>();
            selectionAreaTransform.gameObject.SetActive(false);
        }
        // Update is called once per frame
        void Update()
        {
            
            
            //TODO when selected buidlding then units, slection in UI stuck
            // Selection area
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;

                foreach (Interactable interactableObject in selectedUnitRTSList)
                {
                    interactableObject.gameObject.GetComponent<Interactable>().SetSelectedVisible(false);
                    isSelectedBuilding = false;
                }
                if (selectedUnitRTSList.Find(x => x.GetComponent<BuildingUI>()))
                {
                    selectedUnitRTSList[0].GetComponent<BuildingUI>().SetSelectedVisible(false);
                    isSelectedBuilding = false;

                }
                selectedUnitRTSList.Clear();
                UIHandler.instance.UpdateSelectedUnits();

                // get cursor cordinates, when LPM state is changed
                startPosition = cursorPosition.getMousePosition();
                selectionAreaTransform.gameObject.SetActive(true);
                isSelecting = true;
            }
            if (Input.GetMouseButton(0) && isSelecting == true)
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
            if (Input.GetMouseButtonUp(0) && isSelecting == true)
            {
                // get curosor cordinates, when LPM state is changed, again
                selectionAreaTransform.gameObject.SetActive(false);
                Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosition, cursorPosition.getMousePosition());
                // deslect units
                


                if (collider2DArray.Length==1 && collider2DArray[0].GetComponent<Buildings.Player.PlayerBuilding>()!=null)
                {
                    
                    Interactable building = collider2DArray[0].GetComponent<Interactable>();
                    //BuildingUI building = collider2DArray[0].gameObject.GetComponent<BuildingUI>();
                    building.SetSelectedVisible(true);
                    selectedUnitRTSList.Add(building);
                    isSelectedBuilding = true;
                    isSelecting = false;
                    return;
                }
                
                // select units
                foreach (Collider2D collider2D in collider2DArray)
                {
                    Interactable interactableObject = collider2D.GetComponent<Interactable>();
                    if (interactableObject != null && interactableObject.gameObject.layer == 8)
                    {
                        selectedUnitRTSList.Add(interactableObject);
                        interactableObject.gameObject.GetComponent<Interactable>().SetSelectedVisible(true);
                        
                    }
                }
                UIHandler.instance.UpdateSelectedUnits();
                isSelecting = false;
                // show how manyh objects has been selected
                //Debug.Log(selectedUnitRTSList.Count);
            }


            // Command attack or move
            if (Input.GetMouseButtonDown(1) && selectedUnitRTSList.Count != 0 && !isSelectedBuilding)
            {
                Vector2 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D clicked = Physics2D.Raycast(mousePosition, Vector2.zero);
                if (clicked)
                {
                    Debug.Log(clicked);

                    Transform target = clicked.collider.GetComponent<Transform>();
                    int unitLayer = selectedUnitRTSList[0].gameObject.layer;
                    int targetLayer = clicked.collider.gameObject.layer;
                    if (targetLayer != unitLayer && targetLayer != unitLayer + 1 && targetLayer != 7)
                    {
                        //Debug.Log(selectedUnitRTSList[0].name + " group is going to attack " + target);
                        foreach (Interactable interactableObject in selectedUnitRTSList)
                        {
                            PlayerRTS unitRTS = interactableObject.GetComponent<PlayerRTS>();
                            unitRTS.GetComponent<PlayerRTS>().aggroTarget = target;
                            unitRTS.hasAggro = true;

                            //unitRTS.aggroTarget = clicked.collider.GetComponent<Transform>();
                        }
                        //Debug.Log("Attacking objective is " + selectedUnitRTSList[0].attackObjective);
                    }
                }
                else GroupMove();

            }

            //// when I-key pressed, select all units
            //if (Input.GetKey(KeyCode.I))
            //{
            //    SelectAllUnits();
            //    Debug.Log(selectedUnitRTSList.Count);
            //}

            RaycastHit2D hit = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {

                //Debug.Log(hit.collider.gameObject.tag);
            }
        }
        public void GroupMove()
        {
            ReCommand(selectedUnitRTSList);

            Vector2 moveToPosition = cursorPosition.getMousePosition();
            // TODO: set dynamic vlaues down below

            List<Vector2> targetPositionList = GetPositionListAround(moveToPosition, new float[] { 0.5f, 1, 1.5f, 2f }, new int[] { 5, 10, 20, 40 });
            int targetPositionListIndex = 0;
            foreach (Interactable interactableObject in selectedUnitRTSList)
            {
                Units.UnitRTS unitRTS = interactableObject.GetComponent<Units.UnitRTS>();
                unitRTS.MoveTo(targetPositionList[targetPositionListIndex]);
                targetPositionListIndex = (targetPositionListIndex + 1) % targetPositionList.Count;
            }
        }
        public void ReCommand(List<Interactable> selectedUnits)
        {
            foreach (Interactable interactableObject in selectedUnits)
            {

                PlayerRTS unitRTS = interactableObject.GetComponent<PlayerRTS>();
                try
                {
                    unitRTS.IfCommand = true;
                    unitRTS.aggroTarget = null;
                    unitRTS.hasAggro = false;
                    unitRTS.animator.SetBool("IfAttack", false);
                }
                catch(MissingReferenceException)
                {
                    Debug.Log("Object died");
                }
                
            }
        }
        private void GroupAttack()
        {

        }
        //private void SelectAllUnits()
        //{
        //    Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(new Vector2(-100, -100), new Vector2(1000, 1000));

        //    //deslect units
        //    foreach (PlayerRTS unitRTS in selectedUnitRTSList)
        //    {
        //        unitRTS.gameObject.GetComponent<Interactable>().SetSelectedVisible(false);
        //    }
        //    selectedUnitRTSList.Clear();
        //    //select units
        //    foreach (Collider2D collider2D in collider2DArray)
        //    {
        //        PlayerRTS unitRTS = collider2D.GetComponent<PlayerRTS>();
        //        if (unitRTS != null)
        //        {
        //            unitRTS.gameObject.GetComponent<Interactable>().SetSelectedVisible(true);
        //            selectedUnitRTSList.Add(unitRTS);
        //        }
        //    }
        //}
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

}
