using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movement;

namespace Units.Player
{
    public class PlayerRTS : UnitRTS, IClickable
    {
        public PlayerRTS instance;
        // Start is called before the first frame update
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
            if (IfCommand)
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
        public void Click()
        {
            Debug.Log("Unit");
        }
    }
}

