using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// Functions to show selected units and move groups
public class UnitRTS : MonoBehaviour, IClickable
{
    // Start is called before the first frame update
    
    private GameObject selectedGameObject;
    private IMovePosition movePosition;

    private void Awake()
    {
        selectedGameObject = transform.Find("Selected").gameObject;
        movePosition = GetComponent<IMovePosition>();
        SetSelectedVisible(false);
    }
    public void SetSelectedVisible(bool visible)
    {
        selectedGameObject.SetActive(visible);
    }
    public void MoveTo(Vector3 targetPosition)
    {
        movePosition.SetMovePosition(targetPosition);
    }

    public void Click()
    {
        Debug.Log("Unit");
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger");
    }
}
