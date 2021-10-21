using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    public class EnemyRTS : UnitRTS, IClickable
    {
        public static EnemyRTS instance;

        void Start()
        {
            instance = this;
        }

        void Update()
        {
            if(!hasAggro)
            {
                CheckForEnenmyTargets(baseStats.aggroRange);
            }
            else
            {
                MoveToTarget(aggroTarget.position);
            }
        }

        public void Click()
        {
            Debug.Log("Enemy");
        }

        public int Layer() { return gameObject.layer; }



        public void ImEnemy()
        {

        }
    }
}