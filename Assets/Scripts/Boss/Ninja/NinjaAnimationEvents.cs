using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaAnimationEvents : MonoBehaviour
{
    public GameObject slashPrefab;
    public GameObject slamPrefab;
    public Transform attackPoint;

    public void SpawnSlash()
    {
        GameObject spawn = Instantiate(slashPrefab, attackPoint);
    }

    public void SpawnSlam()
    {
        GameObject spawn = Instantiate(slamPrefab, transform);
        spawn.transform.position -= new Vector3(0, 1, 0);
    }

}
