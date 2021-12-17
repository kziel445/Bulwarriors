using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    /// Functions to show selected units and move groups
    public class UnitRTS : MonoBehaviour
    {
        public static UnitRTS instance;

        //statistics
        public UnitStatTypes.Base baseStats;
        //combat
        public bool isPlayer;
        public IClickable attackObjective; //TODO other class
        private Collider2D[] rangeColliders;
        //change from public
        public Transform aggroTarget;
        //change from public
        public bool hasAggro = false;
        private float distanceToTarget;
        public Transform missile;
        public float atkCooldown;
        
        //animation
        public Animator animator;
        //movement
        public bool IfCommand = false;
        internal IMovePosition movePosition;

        private void Start()
        {
            instance = this;
        }
        //movement segment
        public void MoveTo(Vector3 targetPosition)
        {
            movePosition.SetMovePosition(targetPosition);
        }
        public void MoveToTarget(Transform targetPosition)
        {
            distanceToTarget = DistanceBetweenColliders(targetPosition);
            if (distanceToTarget > baseStats.atkRange) MoveTo(targetPosition.position);
            else MoveTo(transform.position);
        }
        //for now, function check for random enemy(probably close to "0,0")
        internal void CheckForEnenmyTargets(float aggroRange)
        {
            rangeColliders = Physics2D.OverlapCircleAll(transform.position, aggroRange);

            Transform aggroTmp = null;
            for (int i = 0; i < rangeColliders.Length; i++)
            {
                if(aggroTmp==null &&
                    rangeColliders[i].gameObject.layer != gameObject.layer &&
                    rangeColliders[i].gameObject.layer != gameObject.layer + 1 &&
                    rangeColliders[i].gameObject.layer != 7 //7 is world obstacles
                    )
                {
                    aggroTmp = rangeColliders[i].gameObject.transform;
                }
                else if (rangeColliders[i].gameObject.layer != gameObject.layer &&
                    rangeColliders[i].gameObject.layer != gameObject.layer + 1 &&
                    rangeColliders[i].gameObject.layer != 7 &&
                    Vector2.Distance(aggroTmp.transform.position,gameObject.transform.position)>
                    Vector2.Distance(rangeColliders[i].gameObject.transform.position, gameObject.transform.position)
                    )
                {
                    aggroTmp = rangeColliders[i].gameObject.transform;
                }
            }
            aggroTarget = aggroTmp;
            if (aggroTarget != null) hasAggro = true;
        }
        public void Attack()
        {
            if (aggroTarget != null)
            {
                if (atkCooldown <= 0 && distanceToTarget <= baseStats.atkRange)
                {
                    AttackAnimation(true);
                    aggroTarget.GetComponentInChildren<Core.HealthHandler>().TakeDamage(baseStats.damage);
                    atkCooldown = baseStats.atkSpeed;
                }
            }
            else AttackAnimation(false);
        }
        public void FollowAndAttack()
        {
            //lost aggro
            if (aggroTarget != null && Vector2.Distance(aggroTarget.position, gameObject.transform.position) > baseStats.aggroRange * 2)
            {
                MoveToTarget(gameObject.transform);
                aggroTarget = null;
                AttackAnimation(false);
                hasAggro = false;
            }
            //follow and attack
             else if (aggroTarget != null)
            {
                MoveToTarget(aggroTarget);
                Attack();
            }
            //target lost/null
            else
            {
                AttackAnimation(false);
                hasAggro = false;
            }       
        }
        public void AttackAnimation(bool TurnOn)
        {
            if (TurnOn)
            {
                float attackDirection = 0;
                animator.SetBool("IfAttack", true);

                attackDirection = aggroTarget.position.x - gameObject.transform.position.x;
                if (attackDirection > 0) attackDirection = 1;
                else attackDirection = -1;
                animator.SetFloat("AttackDirection", attackDirection);
            }
            else animator.SetBool("IfAttack", false);
        }
        public float DistanceBetweenColliders(Transform targetObject)
        {
            var targetColliders = targetObject.GetComponents<Collider2D>();
            float tmpDistance = -1;
            foreach (Collider2D collider in targetColliders)
            {
                if (tmpDistance == -1)
                    distanceToTarget = Physics2D.Distance(gameObject.GetComponent<Collider2D>(), collider).distance;
                tmpDistance = Physics2D.Distance(gameObject.GetComponent<Collider2D>(), collider).distance;
                if (tmpDistance < distanceToTarget) distanceToTarget = tmpDistance;
            }
            return distanceToTarget;
        }
    }
}
