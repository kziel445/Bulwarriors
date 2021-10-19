using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    ///
    public class UnitHandler : MonoBehaviour
    {
        public static UnitHandler instance;
        [SerializeField]
        private BasicUnit worker, warrior, archer;

        private void Start()
        {
            instance = this;
        }

        void Update()
        {

        }

        public (int damage, int range, int armor, int health, float speed, int cost) getUnitStats(string type)
        {
            BasicUnit unit;
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
                    return (0, 0, 0, 0, 0, 0);
            }
            return (unit.damage, unit.range, unit.armor, unit.health, unit.speed, unit.cost);
        }
        public void setUnitStats(Transform type)
        {
            foreach(Transform child in type)
            {
                foreach(Transform unit in child)
                {
                    string unitName = child.name.Substring(0, child.name.Length - 1).ToLower();
                    var stats = getUnitStats(unitName);
                    UnitRTS playerUnit = unit.GetComponent<UnitRTS>();
                    playerUnit.damage = stats.damage;
                    playerUnit.range = stats.range;
                    playerUnit.armor = stats.armor;
                    playerUnit.health = stats.health;
                    playerUnit.speed = stats.speed;
                    playerUnit.cost = stats.cost;

                }
            }
        }
    }
}