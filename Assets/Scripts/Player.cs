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
    public float SlideMultiplier = 1.2f;
    public float MovementSpeed = 5f;
    public float MovementAcceleration = 0.5f;
    public float ReleaseSprintDelay = 0.5f;
    public float SlideDuration = 0.8f;

    private Rigidbody2D rb;
    private Animator ani;
    private Vector2 movementDirection;
    private Vector2 lastInputDirection; // used for sliding
    private float releaseSprintTimer = 0f;
    private float releaseSlideTimer = 0f;
    private void Awake()
    {
        PlayerMovementState = MovementState.Neutral;
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        Vector3 pos, maxVelocity;
        switch (PlayerMovementState)
        {
            case MovementState.Neutral:
                ani.SetBool("Slide", false);
                ani.SetBool("Block", false);
                maxVelocity = movementDirection * MovementSpeed * Time.deltaTime;
                pos = Vector3.MoveTowards(transform.position, transform.position + maxVelocity, MovementAcceleration);
                rb.MovePosition(pos);
                if (movementDirection != Vector2.zero)
                    ani.SetBool("Run", true);
                else
                    ani.SetBool("Run", false);
                break;
            case MovementState.Block:
                ani.SetBool("Run", false);
                ani.SetBool("Slide", false);
                // Stand still
                ani.SetBool("Block", true);
                break;
            case MovementState.Slide:
                ani.SetBool("Run", false);
                ani.SetBool("Block", false);
                releaseSlideTimer += Time.deltaTime;
                maxVelocity = lastInputDirection * MovementSpeed * SprintMultiplier * SlideMultiplier * Time.deltaTime;
                pos = Vector3.MoveTowards(transform.position, transform.position + maxVelocity, MovementAcceleration);
                rb.MovePosition(pos);
                ani.SetBool("Slide", true);
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
        if(movementDirection.magnitude > 0) lastInputDirection = movementDirection;

        if (Input.GetButton("Sprint") && PlayerMovementState == MovementState.Neutral) // Left shift to sprint
        {
            PlayerMovementState = MovementState.Sprint;
        }
        else if(releaseSprintTimer >= ReleaseSprintDelay) // Release sprint (slight delay for easier sliding)
		{
            PlayerMovementState = MovementState.Neutral;
            releaseSprintTimer = 0;
        }

        bool block = Input.GetButton("Block"); // Left ctrl to block
        if(block && PlayerMovementState == MovementState.Neutral) PlayerMovementState = MovementState.Block;
        if(PlayerMovementState == MovementState.Block && !block) PlayerMovementState = MovementState.Neutral;

        if(PlayerMovementState == MovementState.Sprint && Input.GetButton("Block")) // Start a slide
		{
            // TODO: Redo this however we want. For now it just sets speed to 2x sprint speed and decelerates to zero.
            PlayerMovementState = MovementState.Slide;
		}
        if(PlayerMovementState == MovementState.Slide && releaseSlideTimer >= SlideDuration)
		{
            releaseSlideTimer = 0;
            if (block) PlayerMovementState = MovementState.Block;
            else PlayerMovementState = MovementState.Neutral;
		}

    }
}
