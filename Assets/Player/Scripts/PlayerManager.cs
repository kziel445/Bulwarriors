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

        }
        private void Start()
        {
            instance = this;
            Debug.Log(instance);
            Debug.Log(Units.UnitHandler.instance);

            Units.UnitHandler.instance.setUnitStats(playerUnits);
            Units.UnitHandler.instance.setUnitStats(enemyUnits);

        }

        private void Update()
        {
           
        }
    }
}

