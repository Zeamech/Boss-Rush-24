using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static GolemControler;

public class NinjaTrioControler : MonoBehaviour
{
    GameObject ninja1;
    GameObject ninja2;
    GameObject ninja3;

    [SerializeField]GameObject[] ninjaList;

    public GameObject NinjaPrefab1;
    public GameObject NinjaPrefab2;
    public GameObject NinjaPrefab3;
    public NinjaState ninjaStates;
    public List<NinjaState> ninjaStateList = new List<NinjaState>();


    [SerializeField] private float stateSwitchTimer = 30;
    [SerializeField] private float stateSwitchTimerMx = 30;

    private float healthTracker;
    private float healthStored;

    private bool stage0;
    private bool stage1;
    private bool stage2;
    private bool stage3;
    private bool stage4;

    private float dashTimer = 3;
    private float dashTimerMx = 3;
    private float jumpTimer;
    private float dropTimer;
    private float throwTimer = 1;
    private float throwCD;
    private float throwCDMx = .5f;
    private int throwCOunt;
    private int throwCOuntMx = 3;
    private bool allInAir;
    public int ninjaRef;

    private void Start()
    {
        ninja1 = spawnNinja(NinjaPrefab1);
        ninja2 = spawnNinja(NinjaPrefab2);
        ninja3 = spawnNinja(NinjaPrefab3);
        ninjaList[0] = ninja1;
        ninjaList[1] = ninja2;
        ninjaList[2] = ninja3;

        ninjaStateList.Add(NinjaState.DashAttacks);
        ninjaStates = NinjaState.DashAttacks;
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
                    dashTimer = dashTimerMx;
                    ninjaRef += 1;
                    if (ninjaRef > ninjaList.Length - 1)
                    {
                        ninjaRef = 0;
                        RandomSwitchState(3);
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
                        throwCD = throwCDMx;
                        if (throwCOunt >= throwCOuntMx)
                        {
                            throwCOunt = 0;
                            ninjaRef += 1;
                            throwTimer = 1;
                            RandomSwitchState(3);
                            if (ninjaRef > ninjaList.Length - 1)
                            {
                                ninjaRef = 0;
                                RandomSwitchState(2);
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
                        Debug.Log("all jumped");
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
                        Debug.Log("all slamed");
                        ninjaRef = 0;
                        allInAir = false;
                        RandomSwitchState(3);
                    }
                }

                jumpTimer -= 1 * Time.deltaTime;
                dropTimer -= 1 * Time.deltaTime;
                break;
        }

        healthTracker = GetComponent<HealthBar>().currentHealth;

        if (healthTracker <= GetComponent<HealthBar>().MaxHealth / 1.1f && !stage0)
        {
            ninjaStateList.Add(NinjaState.ThrowingAttack);
            stage0 = true;
        }

        if (healthTracker <= GetComponent<HealthBar>().MaxHealth / 1.5f && !stage1)
        {
            dashTimerMx = 2f;
            throwCOuntMx += 1;
            ninjaStateList.Add(NinjaState.JumpDives);
            SetSwitchState(2);
            stage1 = true;
        }

        if (healthTracker <= GetComponent<HealthBar>().MaxHealth / 2 && !stage2)
        {
            //intensify music
            dashTimerMx = 1f;
            throwCOuntMx += 1;
            throwCDMx = 0.3f;
            stage2 = true;
        }

        if (healthTracker <= GetComponent<HealthBar>().MaxHealth / 6 && !stage3)
        {
            throwCOuntMx += 1;
            dashTimerMx = .5f;
            throwCDMx = 0.1f;
            stage3 = true;
        }

        stateSwitchTimer -= Time.deltaTime;
        if (stateSwitchTimer <= 0)
        {
            SwitchState();
        }
    }

    public void RandomSwitchState(int randomRange)
    {
        if(Random.Range(0, randomRange) <= 0)
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
            ninja.isSlashing = false;
        }

        stateSwitchTimer = stateSwitchTimerMx;
        ninjaStates = ninjaStateList[Random.Range(0, ninjaStateList.Count)];
    }

    public void SetSwitchState(int stateID)
    {
        for (int i = 0; i < ninjaList.Length; i++)
        {
            NinjaController ninja = ninjaList[i].GetComponent<NinjaController>();
            if (ninja.isInAir) ninja.RunSlam();
            ninja.isSlashing = false;
        }

        stateSwitchTimer = stateSwitchTimerMx;
        ninjaStates = ninjaStateList[stateID];
    }

    public GameObject spawnNinja(GameObject prefab)
    {
        Vector3 spawnAdjust = new Vector3(Random.Range(-10, 10), Random.Range(-5, 5), 0);
        GameObject ninja = Instantiate(prefab);
        ninja.transform.position += spawnAdjust;
        ninja.GetComponent<HealthBar>().healthbarHead = GetComponent<HealthBar>();
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
