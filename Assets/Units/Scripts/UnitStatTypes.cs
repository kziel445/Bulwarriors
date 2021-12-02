using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    public class UnitStatTypes : ScriptableObject
    {
        [System.Serializable]
        public class Base
        {
            //
            public float cost;
            //attack stats
            public float damage, atkSpeed, atkRange, aggroRange;
            //other stats
            public float health, armor, movementSpeed;
        }
    }
}

