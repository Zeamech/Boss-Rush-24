using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnAttackController : MonoBehaviour
{

    public bool stunDuration;
    public float LifeTime = 0.3f;


    void Update()
    {
        LifeTime -= Time.deltaTime;
        if(LifeTime <= 0) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.GetComponent<HealthBar>() != null && collision.GetComponent<Player>() != null)
        {

            collision.GetComponent<HealthBar>().AlterHealth(-10);
        }
    }

}
