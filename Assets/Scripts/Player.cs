using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public enum MovementState
    {
        None,
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
    public Slider staminaBarSlider;
    public GameObject PlayerAim;
    public GameObject PlayerTarget;

    public float SprintMultiplier = 1.5f;
    public float SlideMultiplier = 1.2f;
    public float MovementSpeed = 5f;
    public float MovementAcceleration = 0.5f;
    public float ReleaseSprintDelay = 0.5f;
    public float SlideDuration = 0.8f;
    public float AttackDuration = 0.3f;
    public float playerStamina;
    public float playerStaminaMx = 100;

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

    private void Start()
    {
        playerStamina = playerStaminaMx;
    }

    private void FixedUpdate()
    {
        if(movementDirection != Vector2.zero) PlayerAim.transform.localRotation = Quaternion.FromToRotation(Vector3.right, movementDirection);
        ParticleSystem.EmissionModule pEM = PlayerParticleSystem.emission;
        Vector3 pos, maxVelocity;
        if (movementDirection.x > 0)
            ani.transform.localScale = new Vector2(1, ani.transform.localScale.y);
        if (movementDirection.x < 0)
            ani.transform.localScale = new Vector2(-1, ani.transform.localScale.y);
        switch (PlayerMovementState)
        {
            case MovementState.Neutral:
                ani.SetBool("Block", false);
                ani.SetBool("Sprint", false);
                maxVelocity = movementDirection * MovementSpeed * Time.deltaTime;
                pos = Vector3.MoveTowards(transform.position, transform.position + maxVelocity, MovementAcceleration);
                rb.MovePosition(pos);
                if (movementDirection != Vector2.zero)
                {
                    pEM.rateOverTime = 30;
                    ani.SetBool("Run", true);
                }
                else
                {
                    ani.SetBool("Run", false);
                    pEM.rateOverTime = 0;
                }
                break;

            case MovementState.Block:
                HealthBar health = GetComponent<HealthBar>();
                if(playerStamina > 10) 
                    health.isInvulnerable = true;
                else 
                    health.isInvulnerable = false;
                    
                pEM.rateOverTime = 0;
                rb.MovePosition(transform.position);
                ani.SetBool("Run", false);
                ani.SetBool("Sprint", false);
                // Stand still
                ani.SetBool("Block", true);
                break;

            case MovementState.Slide:
                GetComponent<HealthBar>().isInvulnerable = true;
                pEM.rateOverTime = 80;
                ani.SetBool("Run", false);
                ani.SetBool("Sprint", false);
                ani.SetBool("Block", false);
                releaseSlideTimer += Time.deltaTime;
                maxVelocity = lastInputDirection * MovementSpeed * SprintMultiplier * SlideMultiplier * Time.deltaTime;
                pos = Vector3.MoveTowards(transform.position, transform.position + maxVelocity, MovementAcceleration);
                rb.MovePosition(pos);
                ani.SetBool("Slide", true);
                break;

            case MovementState.Sprint:
                ani.SetBool("Sprint", true);
                playerStamina -= 5 * Time.deltaTime;
                pEM.rateOverTime = 50;
                releaseSprintTimer += Time.deltaTime;
                maxVelocity = movementDirection * MovementSpeed * SprintMultiplier * Time.deltaTime;
                pos = Vector3.MoveTowards(transform.position, transform.position + maxVelocity, MovementAcceleration);
                rb.MovePosition(pos);
                break;

            case MovementState.Attack:
                pEM.rateOverTime = 0;
                rb.MovePosition(transform.position);
                if (attackTriggered == false)
                {
                    ani.SetBool("Block", false);
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

            case MovementState.None:
                ani.SetBool("Block", false);
                ani.SetBool("Sprint", false);
                ani.SetBool("Run", false);
                break;

        }

        if (PlayerMovementState != MovementState.Sprint && playerStamina <= playerStaminaMx)
        {
            playerStamina += 5 * Time.deltaTime;
        }
    }

    private void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        if(movementDirection.magnitude > 0) lastInputDirection = movementDirection;

        if (Input.GetButton("Sprint") && PlayerMovementState == MovementState.Neutral && playerStamina > 0) // Left shift to sprint
        {
            PlayerMovementState = MovementState.Sprint;
        }
        else if(releaseSprintTimer >= ReleaseSprintDelay) // Release sprint (slight delay for easier sliding)
		{
            PlayerMovementState = MovementState.Neutral;
            releaseSprintTimer = 0;
        }

        if(PlayerMovementState == MovementState.Sprint && playerStamina <= 0)
        {
            PlayerMovementState = MovementState.Neutral;
        }

        bool block = Input.GetButton("Block"); // Left ctrl to block
        if (block && PlayerMovementState == MovementState.Neutral)
        {
            PlayerMovementState = MovementState.Block;
        }

        if (PlayerMovementState == MovementState.Block && !block)
        {
            PlayerMovementState = MovementState.Neutral;
            GetComponent<HealthBar>().isInvulnerable = false;
        }

        if (PlayerMovementState == MovementState.Sprint && Input.GetButton("Block")) // Start a slide
		{
            // TODO: Redo this however we want. For now it just sets speed to 2x sprint speed and decelerates to zero.
            PlayerMovementState = MovementState.Slide;
		}

        if(PlayerMovementState == MovementState.Slide && releaseSlideTimer >= SlideDuration)
		{
            GetComponent<HealthBar>().isInvulnerable = false;
            ani.SetBool("Slide", false);
            releaseSlideTimer = 0;
            if (block) PlayerMovementState = MovementState.Block;
            else PlayerMovementState = MovementState.Neutral;
		}

        if(Input.GetMouseButtonDown(0) && PlayerMovementState != MovementState.Attack) 
        {
            PreAttackState = PlayerMovementState;
            PlayerMovementState = MovementState.Attack;
            AttackDuration = EquippedWeapon.attackCoolsown;
        }

        if (PlayerMovementState == MovementState.Attack && AttackDuration <= 0)
        {
            switch (PreAttackState)
            {
                case MovementState.Neutral:
                    AttackDuration = EquippedWeapon.BasicAttack.AttackDuration();
                    break;
                case MovementState.Sprint:
                    AttackDuration = EquippedWeapon.SprintAttack.AttackDuration();
                    break;
                case MovementState.Slide:
                    AttackDuration = EquippedWeapon.SlideAttack.AttackDuration();
                    GetComponent<HealthBar>().isInvulnerable = false;
                    ani.SetBool("Slide", false);
                    break;
                case MovementState.Block:
                    AttackDuration = EquippedWeapon.BlockAttack.AttackDuration();
                    break;
            }

            PlayerMovementState = MovementState.Neutral;
            attackTriggered = false;
        }

        UpdateBar(playerStaminaMx, playerStamina);
    }

    public void UpdateBar(float max, float current)
    {
        staminaBarSlider.maxValue = max;
        staminaBarSlider.value = current;
    }
}
