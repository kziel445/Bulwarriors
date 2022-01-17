using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Movement
{
    public class MovePosition : MonoBehaviour, IMovePosition
    {
        public Vector2 movePosition;
        public Animator animator;
        public Vector2 movement;
        private AIPath aiPath;
        private Vector2 lastPosition;

        public void Awake()
        {
            movePosition = transform.position;
            aiPath = gameObject.GetComponent<AIPath>();
        }
        
        public void SetMovePosition(Vector2 movePosition)
        {
            this.movePosition = movePosition;
            aiPath.destination = movePosition;
        }

        private void Update()
        {
            movement.x = aiPath.targetDirection.x;
            movement.y = aiPath.targetDirection.y;
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.y/1000);

            if (Vector2.Distance(movePosition, transform.position) < 0.2f)
            {
                Vector2 moveDir = Vector2.zero;
                movement.x = moveDir.x;
                movement.y = moveDir.y;
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("Speed", movement.sqrMagnitude);
            }
        }
    }

}
