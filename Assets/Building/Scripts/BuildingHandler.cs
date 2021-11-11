using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buildings
{
    public class BuildingHandler : MonoBehaviour
    {
        public static BuildingHandler instance;
        [SerializeField]
        private BuildingBasic house, barracks;
        private void Awake()
        {
            instance = this;
        }
        public BuildingStatTypes.Base GetBuildingStats(string type)
        {
            BuildingBasic building;
            switch (type)
            {
                case "barrack":
                    building = barracks;
                    break;
                case "house":
                    building = house;
                    break;
                default:
                    Debug.Log($"Building Type: {type} could not be found or does not exist!");
                    return null;
            }
            return building.baseStats;
        }
        //public void SetUnitStats(Transform type)
        //{
        //    Transform playerUnits = PlayerManager.instance.playerUnits;
        //    Transform enemyUnits = PlayerManager.instance.enemyUnits;

        //    foreach (Transform child in type)
        //    {
        //        foreach (Transform unit in child)
        //        {
        //            string unitName = child.name.Substring(0, child.name.Length - 1).ToLower();

        //            if (type == playerUnits)
        //            {
        //                Player.PlayerRTS playerUnit = unit.GetComponent<Player.PlayerRTS>();
        //                playerUnit.baseStats = GetUnitStats(unitName);
        //            }
        //            else if (type == enemyUnits)
        //            {
        //                Enemy.EnemyRTS enemyUnit = unit.GetComponent<Enemy.EnemyRTS>();
        //                enemyUnit.baseStats = GetUnitStats(unitName);
        //            }
        //        }
        //    }
        //}
    }
}
