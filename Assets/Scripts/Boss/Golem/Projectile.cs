using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject TargetObject;
    public float projSpeed;
    private float LifeTIme = 3;

    void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(TargetObject.transform.position.x, TargetObject.transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, projSpeed * Time.deltaTime);

        LifeTIme -= Time.deltaTime;
        if(LifeTIme <= 0)
            Destroy(gameObject);
    }
}
