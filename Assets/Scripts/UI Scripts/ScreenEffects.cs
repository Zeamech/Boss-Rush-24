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

    public void ScreenWipe(bool value)
    {
        GetComponent<Animator>().SetBool("Wipe", value);
    }

}
