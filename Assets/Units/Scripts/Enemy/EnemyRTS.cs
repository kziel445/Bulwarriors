using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Enemy
{
    public class EnemyRTS : UnitRTS, IClickable
    {
        public static EnemyRTS instance;
        private void Awake()
        {
            movePosition = GetComponent<IMovePosition>();
        }

        void Start()
        {
            instance = this;
        }

        void Update()
        {
            if (atkCooldown > 0) atkCooldown = atkCooldown - Time.deltaTime;
            if (!hasAggro)
            {
                CheckForEnenmyTargets(baseStats.aggroRange);
            }
            else
            {
                FollowAndAttack();                
            }
            if (IfCommand && aggroTarget==null) StartCoroutine(CheckIfReturningToBase());
        }

        public IEnumerator CheckIfReturningToBase()
        {
            var positionTmp = gameObject.transform.position;
            yield return new WaitForSeconds(5);
            if (positionTmp == gameObject.transform.position)
            {
                IfCommand = false;
                MoveTo(GameObject.Find("EnemyAI").GetComponent<EnemyUnitAI>().groupPoint);
            }
        }
        
        public void Click()
        {
            Debug.Log("Enemy");
        }
    }
}