using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    /// Functions to show selected units and move groups
    public class UnitRTS : MonoBehaviour, IClickable
    {
        //statistics
        public string unitName;
        public int damage;
        public int range;
        public int armor;
        public int health;
        public float speed;
        public int cost;
        public GameObject unitPrefab;

        public IClickable attackObjective; //TODO other class
        private GameObject selectedGameObject;
        private IMovePosition movePosition;

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
            Debug.Log("Hit: " + damage + " to " + attackObjective);
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
