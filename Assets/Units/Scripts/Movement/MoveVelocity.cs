using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVelocity : MonoBehaviour, IMoveVelocity
{
    [SerializeField] private float moveSpeed;
    private Vector2 velocityVector;
    private Rigidbody2D rigidbody2D;

    public void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        moveSpeed = gameObject.GetComponent<Units.UnitRTS>().baseStats.movementSpeed;

    }
    public void SetVelocity(Vector2 velocityVector)
    {
        this.velocityVector = velocityVector;
    }
    public void FixedUpdate()
    {
        rigidbody2D.velocity = velocityVector * moveSpeed;
    }
}
