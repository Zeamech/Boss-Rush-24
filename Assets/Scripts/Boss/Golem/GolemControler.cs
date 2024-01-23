using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GolemControler : MonoBehaviour
{
    public GameObject GolemHead;
    public GameObject GolemHand0;
    public GameObject GolemHand1;

    public GameObject TargetObject;
    public GameObject Projectle;

    private float laserFireTIme;
    [SerializeField] private bool fireLaser;

    private Animator golemHand0Ani;
    private Animator golemHand1Ani;
    private Animator golemHeadAni;

    private float moveTimer = 4;
    private float slamTimer = .2f;
    private float handRecTime = 3;
    private float moveTimer0 = 4;
    private float slamTimer0 = .2f;
    private float handRecTime0 = 6;

    private float healthTracker;
    private float healthStored;
    private float headMoveTimer = 2;
    [SerializeField] private float HeadChaseDist = 10;
    [SerializeField] private float HeadMoveForce = 20;
    private bool catchPlayer;

    [SerializeField]private float handSpeed = 8;

    [SerializeField]private bool hand0Down = true;
    [SerializeField] private bool hand1Down = true;

    void Start()
    {
        golemHand0Ani = GolemHand0.GetComponent<Animator>();
        golemHand1Ani = GolemHand1.GetComponent<Animator>();
        golemHeadAni = GolemHead.GetComponent<Animator>();
        healthTracker = GetComponent<HealthBar>().currentHealth;
        healthStored = healthTracker;
    }

    void FixedUpdate()
    {
        if(TargetObject != null)
        {
            #region Hand0
            if (!hand0Down)
            {
                golemHand0Ani.SetBool("Hand0Up", true);
                FacePlayer(GolemHand0);

                moveTimer0 -= Time.deltaTime;
                if (moveTimer0 <= 0)
                {
                    slamTimer0 -= Time.deltaTime;
                    if (slamTimer0 <= 0)
                    {
                        handRecTime0 = 3;
                        hand0Down = true;
                    }
                }
                else
                {
                    Vector3 targetPos = new Vector3(TargetObject.transform.position.x, TargetObject.transform.position.y, GolemHand0.transform.position.z);
                    GolemHand0.transform.position = Vector3.Lerp(GolemHand0.transform.position, targetPos, handSpeed * Time.deltaTime);
                }
            }
            else
            {
                golemHand0Ani.SetBool("Hand0Up", false);

                handRecTime0 -= Time.deltaTime;
                if (handRecTime0 <= 0)
                {
                    moveTimer0 = 4;
                    slamTimer0 = 1;
                    hand0Down = false;
                }

            }
            #endregion

            #region Hand1
            if (!hand1Down)
            {
                golemHand1Ani.SetBool("Hand1Up", true);
                FacePlayer(GolemHand1);

                moveTimer -= Time.deltaTime;
                if (moveTimer <= 0)
                {
                    slamTimer -= Time.deltaTime;
                    if (slamTimer <= 0)
                    {
                        handRecTime = 3;
                        hand1Down = true;
                    }
                }
                else
                {
                    Vector3 targetPos = new Vector3(TargetObject.transform.position.x, TargetObject.transform.position.y, GolemHand1.transform.position.z);
                    GolemHand1.transform.position = Vector3.Lerp(GolemHand1.transform.position, targetPos, handSpeed * Time.deltaTime);
                }
            }
            else
            {
                golemHand1Ani.SetBool("Hand1Up", false);

                handRecTime -= Time.deltaTime;
                if (handRecTime <= 0)
                {
                    moveTimer = 4;
                    slamTimer = 1;
                    hand1Down = false;
                }

            }
            #endregion

            //fire laser
            if(laserFireTIme <= 0)
            {
                golemHeadAni.SetTrigger("FirePlasma");
                GameObject spawn = Instantiate(Projectle, GolemHead.transform);
                spawn.transform.parent = null;
                spawn.GetComponent<Projectile>().TargetObject = TargetObject;
                spawn.GetComponent<Projectile>().projSpeed = 4;
                laserFireTIme = 1;
            }

            if(fireLaser)
                laserFireTIme -= Time.deltaTime;

            //slide head Dodge
            healthTracker = GetComponent<HealthBar>().currentHealth;
            if(healthStored != healthTracker)
            {
                headMoveTimer -= Time.deltaTime;
                if(headMoveTimer <= 0)
                {
                    golemHeadAni.SetBool("Sliding", true);
                    GolemHead.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-HeadMoveForce, HeadMoveForce), Random.Range(-HeadMoveForce, HeadMoveForce));
                    healthStored = healthTracker;
                    headMoveTimer = 2;
                }
            }

            //Slide head to player

            Vector2 distance = GolemHead.transform.position - TargetObject.transform.position;
            if(distance.magnitude > HeadChaseDist)
                catchPlayer = true;

            if (catchPlayer)
            {
                golemHeadAni.SetBool("Sliding", true);
                Vector3 targetPos = new Vector3(TargetObject.transform.position.x, TargetObject.transform.position.y, GolemHead.transform.position.z);
                GolemHead.GetComponent<Rigidbody2D>().MovePosition(Vector3.Lerp(GolemHead.transform.position, targetPos, Time.deltaTime));
                if (distance.magnitude < 5)
                    catchPlayer = false;
            }


            if (Mathf.Abs(GolemHead.GetComponent<Rigidbody2D>().velocity.x) < 0.5f && Mathf.Abs(GolemHead.GetComponent<Rigidbody2D>().velocity.y) < 0.5f && !catchPlayer)
            {
                golemHeadAni.SetBool("Sliding", false);
            }

            FacePlayer(GolemHead);
        }
        else
        {
            if (CheckForPlayer() != null)
                TargetObject = CheckForPlayer().gameObject;
        }
    }

    public void FacePlayer(GameObject golemPart)
    {
        if (TargetObject.transform.position.x > golemPart.transform.position.x)
            golemPart.transform.localScale = new Vector3(-1, 1, 1);

        if (TargetObject.transform.position.x < golemPart.transform.position.x)
            golemPart.transform.localScale = new Vector3(1, 1, 1);
    }

    public Player CheckForPlayer()
    {
        return FindObjectOfType<Player>();
    }
}
