using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAudio : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event rageEvent;
    [SerializeField] private AK.Wwise.Event slideEvent;
    [SerializeField] private AK.Wwise.Event firePlasmaEvent;

    public void PostRageEvent()
    {
        rageEvent.Post(gameObject);
    }

    public void PostSlideEvent()
    {
        slideEvent.Post(gameObject);
    }

    public void PoseFirePlasmaEvent()
    {
        firePlasmaEvent.Post(gameObject);
    }
}
