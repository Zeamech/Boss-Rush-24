using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject TargetObject;

    public ProjectionType projectionTypes;

    public float projDamage = 2;
    public float projSpeed;
    private float LifeTIme = 3;

    private Vector3 targetPos;
    private Vector3 targetDir;

    private void Start()
    {
        targetPos = new Vector3(TargetObject.transform.position.x, TargetObject.transform.position.y, transform.position.z);
        targetDir = transform.position - targetPos;
    }

    void FixedUpdate()
    {
        targetPos = new Vector3(TargetObject.transform.position.x, TargetObject.transform.position.y, transform.position.z);

        switch(projectionTypes)
        {
            case ProjectionType.Tracking:
                transform.position = Vector3.MoveTowards(transform.position, targetPos, projSpeed * Time.deltaTime);
                break;
            case ProjectionType.Consistent:
                transform.position -= targetDir.normalized * projSpeed;
                break;
        }

        LifeTIme -= Time.deltaTime;
        if(LifeTIme <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            collision.GetComponent<HealthBar>().AlterHealth(-projDamage);
        }

        if(collision.GetComponent<GolemControler>() == null) Destroy(gameObject);
    }

    public enum ProjectionType
    {
        None,
        Consistent,
        Tracking,
    }
}
