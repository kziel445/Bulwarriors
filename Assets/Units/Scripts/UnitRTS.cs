using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
        
        private Collider2D[] rangeColliders;
        private protected Transform aggroTarget;
        private protected bool hasAggro = false;
        private float distanceToTarget;

        public Image healthBarAmount;
        public float currentHealth;

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
            HandleHealth();
            //OnCollisionEnter(GameObject.Find(attackObjective.ToString()).);
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

        public int Layer() { return gameObject.layer; }

        public void OnCollisionEnter(Collision collision)
        {
            attack();
        }
        //movement segment
        public void MoveTo(Vector3 targetPosition)
        {
            movePosition.SetMovePosition(targetPosition);
        }
        public void MoveToTarget(Vector3 targetPosition)
        {
            if(aggroTarget == null)
            {
                MoveTo(transform.position);
                hasAggro = false;
            }
            else
            {
                //get distanceToTarget, when good range can attack
                distanceToTarget = Vector2.Distance(aggroTarget.position, transform.position);
                //(baseStats.atkRange + 1);
                if (distanceToTarget > baseStats.atkRange) MoveTo(aggroTarget.position);
                else MoveTo(transform.position);
            }
        }
        //for now, function check for random enemy(probably close to "0,0")
        internal void CheckForEnenmyTargets(float aggroRange)
        {
            rangeColliders = Physics2D.OverlapCircleAll(transform.position, aggroRange);
            Debug.Log(rangeColliders);
            for (int i = 0; i < rangeColliders.Length; i++)
            {
                Debug.Log(rangeColliders[i].gameObject.name);
                if (rangeColliders[i].gameObject.layer != gameObject.layer && rangeColliders[i].gameObject.layer != gameObject.layer + 1)
                {
                    aggroTarget = rangeColliders[i].gameObject.transform;
                    hasAggro = true;
                    break;
                }
            }
        }
        // combat segment
        private void HandleHealth()
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
            currentHealth -= damage;
        }
        public void attack()
        {
            //Debug.Log("Hit: " + damage + " to " + attackObjective);
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
