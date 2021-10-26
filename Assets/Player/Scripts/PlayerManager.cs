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
            Units.UnitHandler.instance.SetUnitStats(playerUnits);
            Units.UnitHandler.instance.SetUnitStats(enemyUnits);
        }
        private void Start()
        {
            

        }

        private void Update()
        {
           
        }
    }
}

