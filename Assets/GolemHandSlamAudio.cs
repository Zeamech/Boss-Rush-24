using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemHandSlamAudio : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event handSlamAudio;

    public void PlayHandSlamAudio()
    {
        handSlamAudio.Post(gameObject);
    }
}
