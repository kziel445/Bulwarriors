using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cursor
{
    public class Position
    {
        // Start is called before the first frame update
        public Vector2 getMousePosition()
        {
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }

    }
}

