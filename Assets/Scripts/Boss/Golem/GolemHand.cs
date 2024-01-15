using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemHand : MonoBehaviour
{
    public bool isLethal;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.GetComponent<Player>() != null)
        {
            if(isLethal)collision.collider.GetComponent<HealthBar>().AlterHealth(-30);
        }
    }
}
