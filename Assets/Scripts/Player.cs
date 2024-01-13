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
        Sprint,
        Attack
    }
    public MovementState PlayerMovementState;
    public MovementState PreAttackState;

    public InventoryItemData EquippedWeapon;
    public ParticleSystem PlayerParticleSystem;
    public GameObject PlayerAim;
    public GameObject PlayerTarget;

    public float SprintMultiplier = 1.5f;
    public float SlideMultiplier = 1.2f;
    public float MovementSpeed = 5f;
    public float MovementAcceleration = 0.5f;
    public float ReleaseSprintDelay = 0.5f;
    public float SlideDuration = 0.8f;
    public float AttackDuration = 0.3f;

    private Rigidbody2D rb;
    private Animator ani;
    private Vector2 movementDirection;
    private Vector2 lastInputDirection; // used for sliding
    private float releaseSprintTimer = 0f;
    private float releaseSlideTimer = 0f;
    private bool attackTriggered;
    private void Awake()
    {
        PlayerMovementState = MovementState.Neutral;
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        PlayerAim.transform.localRotation = Quaternion.FromToRotation(Vector3.right, movementDirection); ;
        ParticleSystem.EmissionModule pEM = PlayerParticleSystem.emission;
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
                {
                    pEM.rateOverTime = 30;
                    ani.SetBool("Run", true);
                    if (movementDirection.x > 0)
                        ani.transform.localScale = new Vector2(1, ani.transform.localScale.y);
                    if (movementDirection.x < 0)
                        ani.transform.localScale = new Vector2(-1, ani.transform.localScale.y);
                }
                else
                {
                    ani.SetBool("Run", false);
                    pEM.rateOverTime = 0;
                }
                break;
            case MovementState.Block:
                pEM.rateOverTime = 0;
                ani.SetBool("Run", false);
                ani.SetBool("Slide", false);
                // Stand still
                ani.SetBool("Block", true);
                break;
            case MovementState.Slide:
                pEM.rateOverTime = 80;
                ani.SetBool("Run", false);
                ani.SetBool("Block", false);
                releaseSlideTimer += Time.deltaTime;
                maxVelocity = lastInputDirection * MovementSpeed * SprintMultiplier * SlideMultiplier * Time.deltaTime;
                pos = Vector3.MoveTowards(transform.position, transform.position + maxVelocity, MovementAcceleration);
                rb.MovePosition(pos);
                ani.SetBool("Slide", true);
                break;
            case MovementState.Sprint:
                pEM.rateOverTime = 50;
                releaseSprintTimer += Time.deltaTime;
                maxVelocity = movementDirection * MovementSpeed * SprintMultiplier * Time.deltaTime;
                pos = Vector3.MoveTowards(transform.position, transform.position + maxVelocity, MovementAcceleration);
                rb.MovePosition(pos);
                break;
            case MovementState.Attack:
                pEM.rateOverTime = 0;
                if (attackTriggered == false)
                {
                    ani.SetTrigger("Attack");
                    switch (PreAttackState)
                    {
                        case MovementState.Neutral:
                            EquippedWeapon.BasicAttack.AttackCall(PlayerTarget.transform);
                            break;
                        case MovementState.Sprint:
                            EquippedWeapon.SprintAttack.AttackCall(PlayerTarget.transform);
                            break;
                        case MovementState.Slide:
                            EquippedWeapon.SlideAttack.AttackCall(PlayerTarget.transform);
                            break;
                        case MovementState.Block:
                            EquippedWeapon.BlockAttack.AttackCall(PlayerTarget.transform);
                            break;

                    }
                    
                    attackTriggered = true; 
                } 
                AttackDuration -= Time.deltaTime;
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

        if(Input.GetMouseButtonDown(0) && PlayerMovementState != MovementState.Attack) 
        {
            PreAttackState = PlayerMovementState;
            PlayerMovementState = MovementState.Attack;
            AttackDuration = .1f;
        }

        if (PlayerMovementState == MovementState.Attack && AttackDuration <= 0)
        {
            PlayerMovementState = MovementState.Neutral;
            attackTriggered = false;
        }
    }
}
