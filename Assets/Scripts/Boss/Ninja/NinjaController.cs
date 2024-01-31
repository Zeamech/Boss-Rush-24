using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Projectile;

public class NinjaController : MonoBehaviour
{
    public GameObject TargetObject;
    public GameObject projPrefab;
    public Animator ninjaAni;


    public float slashSpeed;

    private Vector3 targetPos;
    private Vector3 targetDir;

    public bool isInAir;
    public bool isSlashing;
    [SerializeField]private bool slashPlease;

    private float jumpPause = 0.3f;

    private void Start()
    {
        TargetObject = CheckForPlayer().gameObject;
    }

    private void FixedUpdate()
    {

        if (slashPlease)
        {
            if (!isInAir) RunJump();
            else RunSlam();
            slashPlease = false;
        }

        if(isSlashing) transform.position -= targetDir.normalized * slashSpeed;
        if((targetPos - transform.position).magnitude <= 2)
        {
            isSlashing = false;
            ninjaAni.SetTrigger("Slash");
        }

        if (isInAir)
        {
            jumpPause -= Time.deltaTime;
            if(jumpPause <= 0)
            {
                Vector3 targetPos = new Vector3(TargetObject.transform.position.x, TargetObject.transform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, targetPos, 10 * Time.deltaTime);
            }
        }
        else jumpPause = 0.3f;

    }

    public void ThrowAttack()
    {
        FacePlayer();
        ninjaAni.SetTrigger("FireStar");
        GameObject spawn = Instantiate(projPrefab, transform);
        spawn.transform.parent = null;
        spawn.GetComponent<Projectile>().TargetObject = TargetObject;
        spawn.GetComponent<Projectile>().projSpeed = 0.4f;
        spawn.GetComponent<Projectile>().projectionTypes = ProjectionType.Consistent;

        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-8, 8), Random.Range(-8, 8));
    }

    public void StartSlash()
    {
        FacePlayer();
        targetPos = new Vector3(TargetObject.transform.position.x, TargetObject.transform.position.y, transform.position.z);
        targetDir = transform.position - targetPos;
        isSlashing = true;
        ninjaAni.SetTrigger("Dash");
    }

    public void RunJump()
    {
        isInAir = true;
        GetComponent<HealthBar>().isInvulnerable = true;
        ninjaAni.SetBool("InAir", true);
    }

    public void RunSlam()
    {
        isInAir = false;
        GetComponent<HealthBar>().isInvulnerable = false;
        ninjaAni.SetBool("InAir", false);
    }

    public Player CheckForPlayer()
    {
        return FindObjectOfType<Player>();
    }

    public void FacePlayer()
    {
        if (TargetObject.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);

        if (TargetObject.transform.position.x < transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
    }
}
