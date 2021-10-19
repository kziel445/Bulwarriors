using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Units
{
    [CreateAssetMenu(fileName = "New Unit", menuName = "New Unit")]
    public class Unit : ScriptableObject
    {
        public enum unitType
        {
            Worker, Warrior, Archer
        }

        //statistics
        [Header("Unit Settings")]
        public string unitName;
        public unitType type;
        public GameObject unitPrefab;

        [Header("Unit Stats")]
        public int damage;
        public int range;
        public int armor;
        public int health;
        public float speed;
        public int cost;
    }
}

