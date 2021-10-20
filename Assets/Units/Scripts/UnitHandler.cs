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
        private UnitTemplate worker, warrior, archer;
        private void Awake()
        {
            Debug.Log("done!!!!");
            instance = this;

        }
        private void Start()
        {
            
        }
        public (float cost, float damage, float aggroRange, float atkRange, float health, float armor, float speed) getUnitStats(string type)
        {
        UnitTemplate unit;
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
                    return (0, 0, 0, 0, 0, 0, 0);
            }
            return (unit.baseStats.cost, unit.baseStats.damage, unit.baseStats.aggroRange, unit.baseStats.atkRange, unit.baseStats.health, unit.baseStats.armor, unit.baseStats.speed);
        }
        public void setUnitStats(Transform type)
        {
            Transform playerUnits = PlayerManager.instance.playerUnits;
            Transform enemyUnits = PlayerManager.instance.enemyUnits;

            foreach (Transform child in type)
            {
                foreach (Transform unit in child)
                {
                    string unitName = child.name.Substring(0, child.name.Length - 1).ToLower();
                    var stats = getUnitStats(unitName);

                    if (type == playerUnits)
                    {
                        UnitRTS playerUnit = unit.GetComponent<UnitRTS>();
                        
                        playerUnit.baseStats.cost = stats.cost;
                        playerUnit.baseStats.damage = stats.damage;
                        playerUnit.baseStats.aggroRange = stats.aggroRange;
                        playerUnit.baseStats.atkRange = stats.atkRange;
                        playerUnit.baseStats.health = stats.health;
                        playerUnit.baseStats.armor = stats.armor;
                        playerUnit.baseStats.speed = stats.speed;
                    }
                    else if (type == enemyUnits)
                    {

                        EnemyRTS enemyUnit = unit.GetComponent<EnemyRTS>();

                        enemyUnit.baseStats.cost = stats.cost;
                        enemyUnit.baseStats.damage = stats.damage;
                        enemyUnit.baseStats.aggroRange = stats.aggroRange;
                        enemyUnit.baseStats.atkRange = stats.atkRange;
                        enemyUnit.baseStats.health = stats.health;
                        enemyUnit.baseStats.armor = stats.armor;
                        enemyUnit.baseStats.speed = stats.speed;
                    }
                }
            }
        }
    }
}