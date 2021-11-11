using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movement;

namespace Units.Player
{
    public class PlayerRTS : UnitRTS, IClickable
    {
        public static PlayerRTS instance;
        
        public bool IfCommand = false;
        // Start is called before the first frame update
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

            if (atkCooldown > 0) atkCooldown = atkCooldown - Time.deltaTime;
            //HandleHealth();
            //player commands

            if (IfCommand)
            {
                if (
                    gameObject.GetComponent<MovePosition>().movement.sqrMagnitude == 0 && 
                    Vector2.Distance(gameObject.GetComponent<MovePosition>().movePosition, transform.position) < 0.2f                    
                    ) IfCommand = false;
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
        //public override void HandleHealth()
        //{

        //    healthBarAmount.fillAmount = currentHealth / baseStats.health;

        //    if (currentHealth <= 0)
        //    {
                
        //        Die();
        //    }
        //}
        public void Click()
        {
            Debug.Log("Unit");
        }
    }
}


