using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum MovementState
    {
        Neutral,
        CrouchShield,
        Slide,
        Sprint
    }
    public MovementState PlayerMovementState;

    public float SprintMultiplier = 1.5f;
    public float MovementSpeed = 5f;
    public float MovementAcceleration = 0.5f;

    private Rigidbody2D rb;
    private Vector2 movementDirection;

    private void Awake()
    {
        PlayerMovementState = MovementState.Neutral;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        switch (PlayerMovementState)
        {
            case MovementState.Neutral:
                Vector2 maxVelocity = movementDirection * MovementSpeed * Time.deltaTime * 100;
                var currentSpeed = Mathf.Min(MovementAcceleration * Time.deltaTime, 1);    // limit to 1 for "full speed"
                Vector3 pos = Vector3.MoveTowards(transform.position, transform.position, MovementSpeed * currentSpeed * Time.deltaTime);
                rb.MovePosition(pos);
                break;
            case MovementState.CrouchShield: 
                
                break;
            case MovementState.Slide: 
                
                break;
            case MovementState.Sprint:
                rb.velocity = movementDirection * (MovementSpeed * SprintMultiplier) * Time.deltaTime * 100;
                break;
        }
    }

    private void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
