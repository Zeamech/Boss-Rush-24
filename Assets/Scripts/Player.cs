using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum MovementState
    {
        Neutral,
        Block,
        Slide,
        Sprint
    }
    public MovementState PlayerMovementState;

    public float SprintMultiplier = 1.5f;
    public float MovementSpeed = 5f;
    public float MovementAcceleration = 0.5f;
    public float ReleaseSprintDelay = 0.5f;

    private Rigidbody2D rb;
    private Vector2 movementDirection;
    private float releaseSprintTimer = 0f;
    private void Awake()
    {
        PlayerMovementState = MovementState.Neutral;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector3 pos, maxVelocity;
        switch (PlayerMovementState)
        {
            case MovementState.Neutral:
                maxVelocity = movementDirection * MovementSpeed * Time.deltaTime;
                pos = Vector3.MoveTowards(transform.position, transform.position + maxVelocity, MovementAcceleration);
                rb.MovePosition(pos);
                break;
            case MovementState.Block:
                // Stand still
                break;
            case MovementState.Slide:

                break;
            case MovementState.Sprint:
                releaseSprintTimer += Time.deltaTime;
                maxVelocity = movementDirection * MovementSpeed * SprintMultiplier * Time.deltaTime;
                pos = Vector3.MoveTowards(transform.position, transform.position + maxVelocity, MovementAcceleration);
                rb.MovePosition(pos);
                break;
        }
    }

    private void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetButton("Sprint") && PlayerMovementState == MovementState.Neutral) // Left shift to sprint
        {
            PlayerMovementState = MovementState.Sprint;
        }
        else if(releaseSprintTimer >= ReleaseSprintDelay) // Release sprint (slight delay for easier sliding)
		{
            PlayerMovementState = MovementState.Neutral;
            releaseSprintTimer = 0;
        }

        bool block = Input.GetButton("Block");
        if(block && PlayerMovementState == MovementState.Neutral)
		{
            PlayerMovementState = MovementState.Block;
		}

        if(PlayerMovementState == MovementState.Block && !block)
		{
            PlayerMovementState = MovementState.Neutral;
		}

    }
}
