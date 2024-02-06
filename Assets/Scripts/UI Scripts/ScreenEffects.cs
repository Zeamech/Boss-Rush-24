using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEffects : MonoBehaviour
{
    public void PlayerHit()
    {
        Camera.main.GetComponent<Animator>().SetTrigger("Shake0");
        GetComponent<Animator>().SetTrigger("Blur");
    }

    public void GolemScream()
    {
        Camera.main.GetComponent<Animator>().SetTrigger("Shake0");
    }
}
