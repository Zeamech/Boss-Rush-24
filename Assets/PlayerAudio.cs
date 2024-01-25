using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event playFootstepEvent;

    public void PlayFootstepAudio()
    {
        playFootstepEvent.Post(gameObject);
    }

    public void CheckforBackgroundTile()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, -vectoe);
    }
}
