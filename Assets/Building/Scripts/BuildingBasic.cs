using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buildings
{
    [CreateAssetMenu(fileName = "Building", menuName = "New Building")]
    public class BuildingBasic : ScriptableObject
    {
        public enum BuildingType
        {
            ArcheryRange, Bank, Barrack, 
            Citadel, Farm, House, 
            ShootingRange, Stable, BlackSmith

        }
        public BuildingType type;
        public new string name;
        public GameObject buildingPrefab;
        public Sprite icon;
        public float spawnTime;

        public BuildingStatTypes.Base baseStats;
        //public BuildingRTS.BuildUnits Units;
         
    }

}
