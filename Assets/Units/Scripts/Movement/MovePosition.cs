using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePosition : MonoBehaviour, IMovePosition
{
    private Vector3 movePosition;
    // public Animator animator;
    private Vector3 movement;
    
    public void Awake()
    {
        movePosition = transform.position;
    }
    // TODO: if this really necessary
    public void SetMovePosition(Vector3 movePosition)
    {
        this.movePosition = movePosition;
    }

    private void Update()
    {
        Vector3 moveDir = (movePosition - transform.position).normalized;
        movement.x = moveDir.x;
        movement.y = moveDir.y;
        // animator.SetFloat("Horizontal", movement.x);
        // animator.SetFloat("Vertical", movement.y);
        // animator.SetFloat("Speed", movement.sqrMagnitude);

        if (Vector3.Distance(movePosition, transform.position) < 0.4f)
        {
            moveDir = Vector3.zero;
            movement.x = moveDir.x;
            movement.y = moveDir.y;
            // animator.SetFloat("Horizontal", movement.x);
            // animator.SetFloat("Vertical", movement.y);
            // animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        GetComponent<IMoveVelocity>().SetVelocity(moveDir);
    }
}
