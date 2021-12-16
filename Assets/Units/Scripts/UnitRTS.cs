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

        public void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("trigger");
        }
        //movement segment
        public void MoveTo(Vector3 targetPosition)
        {
            movePosition.SetMovePosition(targetPosition);
        }

        public void MoveToTarget(Transform targetPosition)
        {
            //MoveTo(transform.position);
            //get distanceToTarget, when good range can attack
            // TODO: Physics2D.Distance(unitRTS.GetComponent<Collider2D>(), clicked.collider); !!!
            distanceToTarget = Vector2.Distance(targetPosition.position, transform.position);
            if(targetPosition.GetComponent<BoxCollider2D>()!=null)
            {
                distanceToTarget -= targetPosition.GetComponent<BoxCollider2D>().bounds.extents.x/2*Mathf.Sqrt(2);
            }
            else if (targetPosition.GetComponent<CircleCollider2D>() != null)
            {
                distanceToTarget -= targetPosition.GetComponent<CircleCollider2D>().bounds.extents.x / 2;
            }
            //(baseStats.atkRange + 1);
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
                    //animator.SetBool("IfAttack", true);
                    //Debug.Log("Hit!");
                    
                    aggroTarget.GetComponentInChildren<Core.HealthHandler>().TakeDamage(baseStats.damage);
                    atkCooldown = baseStats.atkSpeed;
                }
            }
            else AttackAnimation(false);

            //else hasAggro = false;

            //Debug.Log("Hit: " + damage + " to " + attackObjective);
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
            //
             else if (aggroTarget != null)
            {
                MoveToTarget(aggroTarget);
                Attack();
            }
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
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.transform == aggroTarget)
            {

            }
        }

    }
    
    //Debug.Log("collide (name) : " + collide.collider.gameObject.name);
    //Debug.Log("collide (tag) : " + collide.collider.gameObject.tag);
    //if (collide.collider.gameObject.name == "Hitbox")
}
