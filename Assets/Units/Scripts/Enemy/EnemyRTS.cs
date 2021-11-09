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
            selectedGameObject = transform.Find("Selected").gameObject;
            movePosition = GetComponent<IMovePosition>();
            SetSelectedVisible(false);
        }

        void Start()
        {
            instance = this;
            currentHealth = baseStats.health;
        }

        void Update()
        {
            if (atkCooldown > 0) atkCooldown = atkCooldown - Time.deltaTime;
            HandleHealth();
            if (!hasAggro)
            {
                CheckForEnenmyTargets(baseStats.aggroRange);
            }
            else
            {
                FollowAndAttack();                
            }
        }

        public void Click()
        {
            Debug.Log("Enemy");
        }

        public void ImEnemy()
        {

        }
    }
}