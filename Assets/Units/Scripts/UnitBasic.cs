using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{   
    /// BasicUnit
    [CreateAssetMenu(fileName = "New Unit", menuName = "New Unit")]
    public class UnitBasic : ScriptableObject
    {
        public enum unitType
        {
            Worker, Warrior, Archer
        }
        //statistics
        [Header("Unit Settings")]
        public string unitName;
        public unitType type;
        public GameObject playerPrefab;
        public GameObject enemyPrefab;
        public Sprite icon;
        public float spawnTime;

        [Header("Unit Stats")]
        public UnitStatTypes.Base baseStats;
    }
}

