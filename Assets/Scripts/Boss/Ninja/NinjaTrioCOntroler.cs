using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static GolemControler;

public class NinjaTrioCOntroler : MonoBehaviour
{
    GameObject ninja1;
    GameObject ninja2;
    GameObject ninja3;

    [SerializeField]GameObject[] ninjaList;

    public GameObject NinjaPrefab;
    public NinjaState ninjaStates;
    public List<NinjaState> ninjaStateList = new List<NinjaState>();

    [SerializeField] private float stateSwitchTimer;
    [SerializeField] private float stateSwitchTimerMx = 30;

    private float dashTimer = 3;
    private float jumpTimer;
    private float dropTimer;
    private float throwTimer = 1;
    private float throwCD;
    private int throwCOunt;
    private bool allInAir;
    private int ninjaRef;

    private void Start()
    {
        ninja1 = spawnNinja();
        ninja2 = spawnNinja();
        ninja3 = spawnNinja();
        ninjaList[0] = ninja1;
        ninjaList[1] = ninja2;
        ninjaList[2] = ninja3;

        ninjaStateList.Add(NinjaState.DashAttacks);
        
    }

    private void Update()
    {
        switch(ninjaStates)
        {
            case NinjaState.None:
                break;

            case NinjaState.DashAttacks:

                if (dashTimer <= 0)
                {
                    ninjaList[ninjaRef].GetComponent<NinjaController>().StartSlash();
                    dashTimer = 3f;
                    ninjaRef += 1;
                    if (ninjaRef > ninjaList.Length - 1)
                    {
                        ninjaRef = 0;
                        RandomSwitchState();
                    }
                }
                else
                {
                    dashTimer -= Time.deltaTime;
                }

                break;

            case NinjaState.ThrowingAttack:

                throwTimer -= Time.deltaTime;

                if (throwTimer <= 0)
                {
                    throwCD -= 1 * Time.deltaTime;

                    if (throwCD <= 0)
                    {
                        ninjaList[ninjaRef].GetComponent <NinjaController>().ThrowAttack();
                        throwCOunt += 1;
                        throwCD = 0.3f;
                        if (throwCOunt >= 3)
                        {
                            throwCOunt = 0;
                            ninjaRef += 1;
                            throwTimer = 1;
                            if (ninjaRef > ninjaList.Length - 1)
                            {
                                ninjaRef = 0;
                                RandomSwitchState();
                            }
                        }
                    }
                }

                break;

            case NinjaState.JumpDives:

                if (jumpTimer <= 0 && !allInAir)
                {
                    ninjaList[ninjaRef].GetComponent<NinjaController>().RunJump();
                    jumpTimer = 1f;
                    ninjaRef += 1;
                    if (ninjaRef > ninjaList.Length - 1)
                    {
                        ninjaRef = 0;
                        allInAir = true;
                    }
                }

                if (dropTimer <= 0 && allInAir)
                {
                    ninjaList[ninjaRef].GetComponent<NinjaController>().RunSlam();
                    dropTimer = 1f;
                    ninjaRef += 1;
                    if (ninjaRef > ninjaList.Length - 1)
                    {
                        ninjaRef = 0;
                        allInAir = false;
                        RandomSwitchState();
                    }
                }

                jumpTimer -= 1 * Time.deltaTime;
                dropTimer -= 1 * Time.deltaTime;
                break;
        }

        stateSwitchTimer -= Time.deltaTime;
        if (stateSwitchTimer <= 0)
        {
            SwitchState();
        }
    }

    public void RandomSwitchState()
    {
        if(Random.Range(0, 3) <= 0)
        {
            SwitchState();
        }
    }

    public void SwitchState()
    {
        for (int i = 0; i < ninjaList.Length; i++)
        {
            NinjaController ninja = ninjaList[i].GetComponent<NinjaController>();
            if (ninja.isInAir) ninja.RunSlam();
        }

        stateSwitchTimer = stateSwitchTimerMx;
        ninjaStates = ninjaStateList[Random.Range(0, ninjaStateList.Count)];
    }

    public GameObject spawnNinja()
    {
        Vector3 spawnAdjust = new Vector3(Random.Range(-10, 10), Random.Range(-5, 5), 0);
        GameObject ninja = Instantiate(NinjaPrefab);
        ninja.transform.position += spawnAdjust;
        return ninja;
    }

    public enum NinjaState
    {
        None,
        DashAttacks,
        JumpDives,
        ThrowingAttack,
    }
}
