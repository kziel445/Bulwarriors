using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Units;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyRTS : MonoBehaviour, IClickable
    {
        public UnitStatTypes.Base baseStats;
        public void Click()
        {
            Debug.Log("Enemy");
        }

        public int Layer() { return gameObject.layer; }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void ImEnemy()
        {

        }
    }
}