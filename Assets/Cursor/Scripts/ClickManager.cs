using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KZ.Cursor;

public class ClickManager: MonoBehaviour
{
	[SerializeField] private Camera camera;
	[SerializeField] private Texture2D cursor;

    private void Awake()
    {
		Cursor.SetCursor(cursor, Vector3.zero, CursorMode.ForceSoftware);
    }
    void Update()
    {
     	if(Input.GetMouseButtonDown(0))
		{
			Vector2 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D clicked = Physics2D.Raycast(mousePosition, Vector2.zero);
			if(clicked)
			{
				IClickable clickable = clicked.collider.GetComponent<IClickable>();
				clickable?.Click();
			}
		}         
    }

}
