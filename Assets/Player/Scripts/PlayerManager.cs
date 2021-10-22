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
        private void Awake()
        {
            instance = this;
            Units.UnitHandler.instance.setUnitStats(playerUnits);
            Units.UnitHandler.instance.setUnitStats(enemyUnits);
        }
        private void Start()
        {
            

        }

        private void Update()
        {
           
        }
    }
}

