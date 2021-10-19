using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{   
    /// BasicUnit
    [CreateAssetMenu(fileName = "New Unit", menuName = "New Unit")]
    public class UnitTemplate : ScriptableObject
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

        [Header("Unit Stats")]
        public int damage;
        public int range;
        public int armor;
        public int health;
        public float speed;
        public int cost;
    }
}

