using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buildings
{
    public class BuildingHandler : MonoBehaviour
    {
        public static BuildingHandler instance;
        [SerializeField]
        public BuildingBasic house, barrack;
        private void Awake()
        {
            instance = this;
        }
        public BuildingBasic GetBuildingStats(string type)
        {
            BuildingBasic building;
            switch (type)
            {
                case "barrack":
                    building = barrack;
                    break;
                case "house":
                    building = house;
                    break;
                default:
                    Debug.Log($"Building Type: {type} could not be found or does not exist!");
                    return null;
            }
            return building;
        }
    }
}
