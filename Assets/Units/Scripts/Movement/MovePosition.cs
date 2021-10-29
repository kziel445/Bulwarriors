using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePosition : MonoBehaviour, IMovePosition
{
    private Vector2 movePosition;
    // public Animator animator;
    private Vector2 movement;
    
    public void Awake()
    {
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
        // animator.SetFloat("Horizontal", movement.x);
        // animator.SetFloat("Vertical", movement.y);
        // animator.SetFloat("Speed", movement.sqrMagnitude);

        if (Vector2.Distance(movePosition, transform.position) < 0.4f)
        {
            moveDir = Vector2.zero;
            movement.x = moveDir.x;
            movement.y = moveDir.y;
            // animator.SetFloat("Horizontal", movement.x);
            // animator.SetFloat("Vertical", movement.y);
            // animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        GetComponent<IMoveVelocity>().SetVelocity(moveDir);
    }
}
