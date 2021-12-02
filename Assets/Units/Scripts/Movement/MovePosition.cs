using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movement
{
    public class MovePosition : MonoBehaviour, IMovePosition
    {
        private static MovePosition instance;
        public Vector2 movePosition;
        public Animator animator;
        public Vector2 movement;

        public void Awake()
        {
            instance = this;
            movePosition = transform.position;
        }
        // TODO: if this really necessary
        public void SetMovePosition(Vector2 movePosition)
        {
            this.movePosition = movePosition;
        }

        private void Update()
        {
            Vector2 moveDir = (movePosition - new Vector2(transform.position.x, transform.position.y)).normalized;
            movement.x = moveDir.x;
            movement.y = moveDir.y;
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.y/1000);

            if (Vector2.Distance(movePosition, transform.position) < 0.2f)
            {
                moveDir = Vector2.zero;
                movement.x = moveDir.x;
                movement.y = moveDir.y;
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("Speed", movement.sqrMagnitude);
            }
            GetComponent<IMoveVelocity>().SetVelocity(moveDir);
        }
    }

}
