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
            public int cost;
            public float health, armor, attack;
            public UI.PlayerActions actions;
        }
    }

}
