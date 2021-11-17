using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buildings
{
    public class BuildingStatTypes : ScriptableObject
    {
        [System.Serializable] 
        public class Base
        {
            public float health, armor, attack, cost;
        }
    }

}
