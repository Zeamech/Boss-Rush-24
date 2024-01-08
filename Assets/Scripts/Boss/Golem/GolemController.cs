using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemController : MonoBehaviour
{
    public GameObject GolemHand1;
    public GameObject GolemHand2;
    public GameObject GolemHead;

    [SerializeField]private float handMvSpeed = 1;
    [SerializeField] private float slamRange = 1;
    private float raiseTime = 2;
    private float raiseTime2 = 2;
    private float handCooldown = 3;
    private float handCooldown2 = 1.5f;
    private float slamTimer = 1.5f;
    private float slamTimer2 = 1.5f;

    [SerializeField]private GameObject targetObject;
    private Animator golemAni;

    private HandState handState;
    private HandState2 handState2;

    private void Awake()
    {
        golemAni = GetComponent<Animator>();
    }
    
    void Update()
    {
        #region Hand1
        Vector2 targetDir = GolemHand1.transform.position - targetObject.transform.position;
        Vector2 handPos = targetDir.normalized * -handMvSpeed;

        if (handState == HandState.Raised)
        {
            raiseTime -= Time.deltaTime;

            golemAni.SetBool("Floating", true);
            GolemHand1.GetComponent<Rigidbody2D>().velocity = handPos;
            if (targetDir.magnitude <= slamRange)
            {
                GolemHand1.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                if (raiseTime <= 0)
                {
                    handState = HandState.Slam;
                    raiseTime = 2;
                }
            }
        }
       
        if(handState == HandState.Slam)
        {
            GolemHand1.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            slamTimer -= Time.deltaTime;
            if (slamTimer <= 0)
            {
                golemAni.SetTrigger("Slam");
                golemAni.SetBool("Floating", false);
                slamTimer = 1.5f;
                handState = HandState.Down;
            }
        }

        if(handState == HandState.Down)
        {
            GolemHand1.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            handCooldown -= Time.deltaTime;

            if(handCooldown <= 0)
            {
                handState = HandState.Raised;
                handCooldown = 3;
            }
        }
        #endregion

        #region Hand2
        Vector2 targetDir2 = GolemHand2.transform.position - targetObject.transform.position;
        Vector2 handPos2 = targetDir2.normalized * -handMvSpeed;

        if (handState2 == HandState2.Raised)
        {
            raiseTime2 -= Time.deltaTime;

            //golemAni.SetBool("Floating", true);
            GolemHand2.GetComponent<Rigidbody2D>().velocity = handPos2;
            if (targetDir2.magnitude <= slamRange)
            {
                GolemHand2.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                if (raiseTime2 <= 0)
                {
                    handState2 = HandState2.Slam;
                    raiseTime2 = 2;
                }
            }
        }

        if (handState2 == HandState2.Slam)
        {
            GolemHand2.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            slamTimer2 -= Time.deltaTime;
            if (slamTimer2 <= 0)
            {
                golemAni.SetBool("Floating", false);
                slamTimer2 = 1.5f;
                golemAni.SetTrigger("Slam");
                handState2 = HandState2.Down;
            }
        }

        if (handState2 == HandState2.Down)
        {
            GolemHand2.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            handCooldown2 -= Time.deltaTime;

            if (handCooldown2 <= 0)
            {
                handState2 = HandState2.Raised;
                handCooldown2 = 3;
            }
        }
        #endregion

    }

    private enum HandState
    {
        Down,
        Raised,
        Slam,
    }

    private enum HandState2
    {
        Down,
        Raised,
        Slam,
    }
}
