using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movement;

namespace Units.Player
{
    public class PlayerRTS : UnitRTS, IClickable
    {
        public static PlayerRTS instance;
        
        private void Start()
        {
            instance = this;
        }

        private void Awake()
        {
            movePosition = GetComponent<IMovePosition>();
        }

        private void Update()
        {

            if (atkCooldown > 0) atkCooldown = atkCooldown - Time.deltaTime;

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
        }
        
        public void Click()
        {
            Debug.Log("Unit");
        }
    }
}


