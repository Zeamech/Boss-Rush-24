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

    public float moveTimerMx = 3;
    public float slamTimerMx = .1f;
    public float handRecTimeMx = 3;
    public float moveTimer0Mx = 3;
    public float slamTimer0Mx = .1f;
    public float handRecTime0MX = 3;

    private float laserFireTIme;

    private Animator golemHand0Ani;
    private Animator golemHand1Ani;

    private float moveTimer = 3;
    private float slamTimer = .1f;
    private float handRecTime = 3;
    private float moveTimer0 = 3;
    private float slamTimer0 = .1f;
    private float handRecTime0 = 6;

    [SerializeField]private float handSpeed = 8;

    [SerializeField]private bool hand0Down = true;
    [SerializeField] private bool hand1Down = true;

    void Start()
    {
        golemHand0Ani = GolemHand0.GetComponent<Animator>();
        golemHand1Ani = GolemHand1.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(TargetObject != null)
        {
            #region Hand0
            if (!hand0Down)
            {
                golemHand0Ani.SetBool("Hand0Up", true);

                moveTimer0 -= Time.deltaTime;
                if (moveTimer0 <= 0)
                {
                    slamTimer0 -= Time.deltaTime;
                    if (slamTimer0 <= 0)
                    {
                        handRecTime0 = handRecTime0MX;
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
                    moveTimer0 = moveTimer0Mx;
                    slamTimer0 = slamTimer0Mx;
                    hand0Down = false;
                }

            }
            #endregion

            #region Hand1
            if (!hand1Down)
            {
                golemHand1Ani.SetBool("Hand1Up", true);

                moveTimer -= Time.deltaTime;
                if (moveTimer <= 0)
                {
                    slamTimer -= Time.deltaTime;
                    if (slamTimer <= 0)
                    {
                        handRecTime = handRecTimeMx;
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
                    moveTimer = moveTimerMx;
                    slamTimer = slamTimerMx;
                    hand1Down = false;
                }

            }
            #endregion

            if(laserFireTIme <= 0)
            {
                GameObject spawn = Instantiate(Projectle, GolemHead.transform);
                spawn.GetComponent<Projectile>().TargetObject = TargetObject;
                spawn.GetComponent<Projectile>().projSpeed = 4;
                laserFireTIme = 1;
            }

            laserFireTIme -= Time.deltaTime;
            

        }
        else
        {
            if (CheckForPlayer() != null)
                TargetObject = CheckForPlayer().gameObject;
        }
    }

    public Player CheckForPlayer()
    {
        return FindObjectOfType<Player>();
    }
}
