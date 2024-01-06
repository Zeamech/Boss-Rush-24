using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public float StartUpTime;
    public float RecoveryTime;
    public float Duration;
    public bool stun;
    

    private BoxCollider2D hurtbox;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private float currentTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= StartUpTime + Duration + RecoveryTime) Destroy(gameObject);
    }
}
