using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Units
{
    public class UnitHandler : MonoBehaviour
    {
        public static UnitHandler instance;
        [SerializeField]
        private UnitBasic worker, warrior, archer;
        private void Awake()
        {
            instance = this;
        }
        public UnitStatTypes.Base GetUnitStats(string type)
        {
        UnitBasic unit;
            switch(type)
            {
                case "worker":
                    unit = worker;
                    break;
                case "warrior":
                    unit = warrior;
                    break;
                case "archer":
                    unit = archer;
                    break;
                default:
                    Debug.Log($"Unit Type: {type} could not be found or does not exist!");
                    return null;
            }
            return unit.baseStats;
        }
        public void SetUnitStats(Transform type)
        {
            Transform playerUnits = PlayerManager.instance.playerUnits;
            Transform enemyUnits = PlayerManager.instance.enemyUnits;

            foreach (Transform child in type)
            {
                foreach (Transform unit in child)
                {
                    string unitName = child.name.Substring(0, child.name.Length - 1).ToLower();
                    var stats = GetUnitStats(unitName);

                    if (type == playerUnits)
                    {
                        UnitRTS playerUnit = unit.GetComponent<UnitRTS>();
                        playerUnit.baseStats = GetUnitStats(unitName);
                    }
                    else if (type == enemyUnits)
                    {
                        Enemy.EnemyRTS enemyUnit = unit.GetComponent<Enemy.EnemyRTS>();
                        enemyUnit.baseStats = GetUnitStats(unitName);
                    }
                }
            }
        }
    }
}