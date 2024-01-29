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
    private bool isSlashing;
    [SerializeField]private bool slashPlease;

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
            Vector3 targetPos = new Vector3(TargetObject.transform.position.x, TargetObject.transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, 10 * Time.deltaTime);
        }

    }

    public void ThrowAttack()
    {
        ninjaAni.SetTrigger("FireStar");
        GameObject spawn = Instantiate(projPrefab, transform);
        spawn.transform.parent = null;
        spawn.GetComponent<Projectile>().TargetObject = TargetObject;
        spawn.GetComponent<Projectile>().projSpeed = 0.4f;
        spawn.GetComponent<Projectile>().projectionTypes = ProjectionType.Consistent;

        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3, 3), Random.Range(-3, 3));
    }

    public void StartSlash()
    {
        targetPos = new Vector3(TargetObject.transform.position.x, TargetObject.transform.position.y, transform.position.z);
        targetDir = transform.position - targetPos;
        isSlashing = true;
        ninjaAni.SetTrigger("Dash");
    }

    public void RunJump()
    {
        isInAir = true;
        GetComponent<HealthBar>().isInvulnerable = true;
        ninjaAni.SetTrigger("Jump");
    }

    public void RunSlam()
    {
        isInAir = false;
        GetComponent<HealthBar>().isInvulnerable = false;
        ninjaAni.SetTrigger("Drop");
    }

    public Player CheckForPlayer()
    {
        return FindObjectOfType<Player>();
    }


}
