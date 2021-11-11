using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buildings.Player
{
    public class PlayerBuilding : BuildingRTS
    {
        private void Awake()
        {
            selectedGameObject = transform.Find("Selected").gameObject;
            SetSelectedVisible(false);
        }
    }
}