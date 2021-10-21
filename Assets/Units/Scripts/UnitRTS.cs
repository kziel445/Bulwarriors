using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private void Start()
        {
            instance = this;
        }
        private void Awake()
        {
            selectedGameObject = transform.Find("Selected").gameObject;
            movePosition = GetComponent<IMovePosition>();
            SetSelectedVisible(false);
        }
        private void Update()
        {
            //if()
            //OnCollisionEnter(GameObject.Find(attackObjective.ToString()).);

        }
        public void SetSelectedVisible(bool visible)
        {
            selectedGameObject.SetActive(visible);
        }
        public void MoveTo(Vector3 targetPosition)
        {
            movePosition.SetMovePosition(targetPosition);
        }
        public void MoveToTarget(Vector3 targetPosition)
        {
            //get distanceToTarget, when good range can attack
            distanceToTarget = Vector2.Distance(aggroTarget.position, transform.position);
            //(baseStats.atkRange + 1);

            if (distanceToTarget <= baseStats.aggroRange)
            {
                MoveTo(aggroTarget.position);
                //navAgent.SetDestination(aggroTarget.position);
            }

        }

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

        public void attack()
        {
            //Debug.Log("Hit: " + damage + " to " + attackObjective);
        }

        private void OnDestroy()
        {
            Destroy(gameObject);
        }
    }


    //Debug.Log("collide (name) : " + collide.collider.gameObject.name);
    //Debug.Log("collide (tag) : " + collide.collider.gameObject.tag);
    //if (collide.collider.gameObject.name == "Hitbox")
}
