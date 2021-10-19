using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager instance;
        public Transform playerUnits;
        public Transform enemyUnits;
        void Start()
        {
            instance = this;
            Units.UnitHandler.instance.setUnitStats(playerUnits);
            Units.UnitHandler.instance.setUnitStats(enemyUnits);

        }

        void Update()
        {
           
        }
    }
}

