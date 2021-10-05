// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using CodeMonkey.Utils;


/// unused for now
// public class RTSController : MonoBehaviour
// {
//     [SerializeField] private Transform selectionAreaTransform;
//     private Vector3 startPosition;
//     private List<UnitRTS> selectedUnitRTSList;
//     private void Awake()
//     {
//         selectedUnitRTSList = new List<UnitRTS>();
//         selectionAreaTransform.gameObject.SetActive(false);
//     }
//     // Update is called once per frame
//     void Update()
//     {
//         if(Input.GetMouseButtonDown(0))
//         {
//             //pobiera wsp�rz�dne kursora jak stan lewego przycisku myszy si� zmieni na wci�ni�ty
//             startPosition = UtilsClass.GetMouseWorldPosition();
//             selectionAreaTransform.gameObject.SetActive(true);
//         }
//         if(Input.GetMouseButton(0))
//         {
//             //tworzenie pola, kt�re zaznacza jednostki
//             Vector3 currentMousePosition = UtilsClass.GetMouseWorldPosition();
//             Vector3 lowerLeft = new Vector3(
//                 Mathf.Min(startPosition.x, currentMousePosition.x),
//                 Mathf.Min(startPosition.y, currentMousePosition.y)
//                 );
//             Vector3 upperRight = new Vector3(
//                 Mathf.Max(startPosition.x, currentMousePosition.x),
//                 Mathf.Max(startPosition.y, currentMousePosition.y)
//                 );
//             selectionAreaTransform.position = lowerLeft;
//             selectionAreaTransform.localScale = upperRight - lowerLeft;
//         }
//         if(Input.GetMouseButtonUp(0))
//         {
//             //pobiera wsp�rz�dne kurosora jak stan zmieni si� ponownie
//             selectionAreaTransform.gameObject.SetActive(false);
//             //Komenda ni�ej pokazuje wsp�rz�dne kursora na pocz�tku i konca zaznaczenia
//             //Debug.Log(UtilsClass.GetMouseWorldPosition() + " " + startPosition);
//             Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosition,UtilsClass.GetMouseWorldPosition());
//             //deslect units
//             foreach(UnitRTS unitRTS in selectedUnitRTSList)
//             {
//                 unitRTS.SetSelectedVisible(false);
//             }
//             selectedUnitRTSList.Clear();
//             //select units
//             foreach (Collider2D collider2D in collider2DArray)
//             {
//                 //pokazuje jaki obiekt zosta� zaznaczony
//                 //Debug.Log(collider2D);
//                 UnitRTS unitRTS = collider2D.GetComponent<UnitRTS>();
//                 if(unitRTS!=null)
//                 {
//                     unitRTS.SetSelectedVisible(true);
//                     selectedUnitRTSList.Add(unitRTS);
//                 }
//             }
//             //pokazuje ile obiekt�w zosta�o zaznaczone
//             Debug.Log(selectedUnitRTSList.Count);
//         }
//         if (Input.GetMouseButtonDown(1))
//         {
//             Vector3 movePosition = UtilsClass.GetMouseWorldPosition();
//             foreach(UnitRTS unitRTS in selectedUnitRTSList)
//             {
//                 unitRTS.MoveTo(movePosition);
//             }
//         }
//         if (Input.GetKey(KeyCode.I))
//         {
//             Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(new Vector2(-100,-100), new Vector2(1000, 1000));

//             //deslect units
//             foreach (UnitRTS unitRTS in selectedUnitRTSList)
//             {
//                 unitRTS.SetSelectedVisible(false);
//             }
//             selectedUnitRTSList.Clear();
//             //select units
//             foreach (Collider2D collider2D in collider2DArray)
//             {
//                 //pokazuje jaki obiekt zosta� zaznaczony
//                 //Debug.Log(collider2D);
//                 UnitRTS unitRTS = collider2D.GetComponent<UnitRTS>();
//                 if (unitRTS != null)
//                 {
//                     unitRTS.SetSelectedVisible(true);
//                     selectedUnitRTSList.Add(unitRTS);
//                 }
//             }
//             Debug.Log(selectedUnitRTSList.Count);
//         }
//     }
    
// }
