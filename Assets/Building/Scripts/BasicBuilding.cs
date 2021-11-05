using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    [CreateAssetMenu(fileName = "Building", menuName = "New Building/Basic")]
    public class BasicBuilding : ScriptableObject
    {
        public enum BuildingType
        {
            Barracks,
            House
        }
        public BuildingType type;
        public new string name;
        public GameObject buildingPrefab;

        public BuildingStatTypes.Base baseStats;

         
    }

}
