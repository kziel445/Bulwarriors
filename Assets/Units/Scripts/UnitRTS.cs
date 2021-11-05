using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Movement;

namespace Units
{
    /// Functions to show selected units and move groups
    public class UnitRTS : MonoBehaviour, IClickable
    {
        private UnitRTS instance;
        public UnitStatTypes.Base baseStats;
        public IClickable attackObjective; //TODO other class
        private GameObject selectedGameObject;
        private IMovePosition movePosition;
        public Animator animator;
        
        private Collider2D[] rangeColliders;
        //change from public
        public Transform aggroTarget;
        //change from public
        public bool hasAggro = false;
        private float distanceToTarget;

        public Image healthBarAmount;
        public float currentHealth;
        public Transform missile;
        public float atkCooldown;

        public bool IfCommand = false;

        private void Start()
        {
            instance = this;
            currentHealth = baseStats.health;
        }
        private void Awake()
        {
            selectedGameObject = transform.Find("Selected").gameObject;
            movePosition = GetComponent<IMovePosition>();
            SetSelectedVisible(false);
        }
        private void Update()
        {

            if (atkCooldown > 0) atkCooldown = atkCooldown - Time.deltaTime;
            HandleHealth();
            //player commands
            if(IfCommand)
            {
                if (gameObject.GetComponent<MovePosition>().movement.sqrMagnitude == 0) IfCommand = false;
            }
            else
            {
                if (hasAggro)
                {
                    FollowAndAttack();
                }
                else
                {
                    CheckForEnenmyTargets(baseStats.aggroRange);
                }
            }
 
            
            //auto commands
        }

        public void SetSelectedVisible(bool visible)
        {
            selectedGameObject.SetActive(visible);
        }
        public void Click()
        {
            Debug.Log("Unit");
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

        public void MoveToTarget(Vector3 targetPosition)
        {
                MoveTo(transform.position);
                //get distanceToTarget, when good range can attack
                distanceToTarget = Vector2.Distance(aggroTarget.position, transform.position);
                //(baseStats.atkRange + 1);
                if (distanceToTarget > baseStats.atkRange) MoveTo(aggroTarget.position);
                else MoveTo(transform.position);
        }
        //for now, function check for random enemy(probably close to "0,0")
        internal void CheckForEnenmyTargets(float aggroRange)
        {
            rangeColliders = Physics2D.OverlapCircleAll(transform.position, aggroRange);
            for (int i = 0; i < rangeColliders.Length; i++)
            {
                if (rangeColliders[i].gameObject.layer != gameObject.layer && rangeColliders[i].gameObject.layer != gameObject.layer + 1)
                {
                    aggroTarget = rangeColliders[i].gameObject.transform;
                    hasAggro = true;
                    break;
                }
            }
        }
        // combat segment
        public void HandleHealth()
        {
            healthBarAmount.fillAmount = currentHealth / baseStats.health;

            if (currentHealth <= 0)
            {
                if (InputManager.InputHandler.instance.selectedUnitRTSList.Contains(gameObject.GetComponent<UnitRTS>()))
                    {
                    InputManager.InputHandler.instance.selectedUnitRTSList.Remove(gameObject.GetComponent<UnitRTS>());
                    }
                Die();
            }
        } 
        public void TakeDamage(float damage)
        {
            //TODO: do better formula for fight
            damage -= baseStats.armor;
            if (damage <= 0) damage = 1;
            //Debug.Log(damage);
            currentHealth -= damage;
        }
        public void Attack()
        {
            if (aggroTarget != null)
            {
                if (atkCooldown <= 0 && distanceToTarget <= baseStats.atkRange)
                {
                    AttackAnimation(true);
                    animator.SetBool("IfAttack", true);
                    Debug.Log("Hit!");
                    aggroTarget.GetComponent<UnitRTS>().TakeDamage(baseStats.damage);
                    atkCooldown = baseStats.atkSpeed;
                }
            }
            else AttackAnimation(false);

            //else hasAggro = false;

            //Debug.Log("Hit: " + damage + " to " + attackObjective);
        }
        public void FollowAndAttack()
        {
            if (aggroTarget != null)
            {
                MoveToTarget(aggroTarget.position);
                Attack();
            }
            else
            {
                animator.SetBool("IfAttack", false);
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
        public void Die()
        {
            Destroy(gameObject);
        }
    }
    //Debug.Log("collide (name) : " + collide.collider.gameObject.name);
    //Debug.Log("collide (tag) : " + collide.collider.gameObject.tag);
    //if (collide.collider.gameObject.name == "Hitbox")
}
