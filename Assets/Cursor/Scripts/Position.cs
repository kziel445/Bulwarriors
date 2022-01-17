using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KZ.Cursor
{
    public class Position
    {
        public Vector2 getMousePosition()
        {
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }
    }
}

