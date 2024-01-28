using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Projectile;

public class NinjaController : MonoBehaviour
{
    public GameObject TargetObject;

    public float slashSpeed;

    private Vector3 targetPos;
    private Vector3 targetDir;

    private bool isSlashing;
    [SerializeField]private bool slashPlease;

    private void Start()
    {
        TargetObject = CheckForPlayer().gameObject;
    }

    private void FixedUpdate()
    {
        if(slashPlease)
        {
            StartSlash();
            slashPlease = false;
        }

        if(isSlashing) transform.position -= targetDir.normalized * slashSpeed;
        if((targetPos - transform.position).magnitude <= 1) isSlashing = false;
    }

    public void StartSlash()
    {
        targetPos = new Vector3(TargetObject.transform.position.x, TargetObject.transform.position.y, transform.position.z);
        targetDir = transform.position - targetPos;
        isSlashing = true;
    }

    public Player CheckForPlayer()
    {
        return FindObjectOfType<Player>();
    }
}
