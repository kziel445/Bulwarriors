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

        public Transform playerBuildings;
        public Transform enemyBuildings;

        private void Awake()
        {
            instance = this;
            SetStats(playerUnits);
            SetStats(enemyUnits);
            SetStats(playerBuildings);
            SetStats(enemyBuildings);
        }
        private void Start()
        {
            

        }

        private void Update()
        {
           
        }
        public void SetStats(Transform type)
        {
            //Transform playerUnits = PlayerManager.instance.playerUnits;
            //Transform enemyUnits = PlayerManager.instance.enemyUnits;

            foreach (Transform child in type)
            {
                foreach (Transform transformObject in child)
                {
                    string objectName = child.name.Substring(0, child.name.Length - 1).ToLower();

                    if (type == playerUnits)
                    {
                        Units.Player.PlayerRTS playerUnit = transformObject.GetComponent< Units.Player.PlayerRTS >();
                        playerUnit.baseStats = Units.UnitHandler.instance.GetUnitStats(objectName);
                    }
                    else if (type == enemyUnits)
                    {
                        Units.Enemy.EnemyRTS enemyUnit = transformObject.GetComponent<Units.Enemy.EnemyRTS>();
                        enemyUnit.baseStats = Units.UnitHandler.instance.GetUnitStats(objectName);
                    }
                    else if (type == playerBuildings)
                    {
                        Buildings.Player.PlayerBuilding playerBuilding = transformObject.GetComponent<Buildings.Player.PlayerBuilding>();
                        playerBuilding.baseStats = Buildings.BuildingHandler.instance.GetBuildingStats(objectName);

                    }
                    else if (type == enemyBuildings)
                    {
                        Buildings.Enemy.EnemyBuilding enemyBuilding = transformObject.GetComponent<Buildings.Enemy.EnemyBuilding>();
                        enemyBuilding.baseStats = Buildings.BuildingHandler.instance.GetBuildingStats(objectName);
                    }
                }
            }
        }
    }
}

