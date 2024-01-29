using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NinjaTrioCOntroler : MonoBehaviour
{
    GameObject ninja1;
    GameObject ninja2;
    GameObject ninja3;

    [SerializeField]GameObject[] ninjaList;

    public GameObject NinjaPrefab;
    public NinjaState ninjaStates;

    private float dashTimer = 3;
    private float jumpTimer;
    private float dropTimer;
    private float inAirTIme;
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
                    if (ninjaRef > ninjaList.Length - 1) ninjaRef = 0;
                }
                else
                {
                    dashTimer -= Time.deltaTime;
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
                    }
                }

                jumpTimer -= 1 * Time.deltaTime;
                dropTimer -= 1 * Time.deltaTime;
                break;
        }

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
    }
}
