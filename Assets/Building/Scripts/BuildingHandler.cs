using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buildings
{
    public class BuildingHandler : MonoBehaviour
    {
        public static BuildingHandler instance;
        [SerializeField] public BuildingBasic 
            archeryRange, bank, barrack, citadel, 
            farm, house, shootingRange, stable,
            blackSmith;
            
        private void Awake()
        {
            instance = this;
        }

        public BuildingBasic GetBuildingStats(string type)
        {
            type = type.Replace(" ", "");
            BuildingBasic building;
            switch (type)
            {
                case "archeryrange":
                    building = archeryRange;
                    break;
                case "bank":
                    building = bank;
                    break;
                case "barrack":
                    building = barrack;
                    break;
                case "citadel":
                    building = citadel;
                    break;
                case "farm":
                    building = farm;
                    break;
                case "house":
                    building = house;
                    break;
                case "shootingrange":
                    building = shootingRange;
                    break;
                case "stable":
                    building = stable;
                    break;
                case "blacksmith":
                    building = blackSmith;
                    break;
                default:
                    Debug.LogWarning($"Building Type: {type} could not be found or does not exist!");
                    return null;
            }
            return building;
        }
    }
}
