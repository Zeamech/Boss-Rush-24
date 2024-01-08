 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamController : MonoBehaviour {

    public Transform targetT;

    [SerializeField]
    private Image screenBlur;
    public float blurAlpha;

    public float camSpd = 2;

    private Animator camAni;

    private void Start()
    {
        //camAni = GetComponent<Animator>();
    }

    private void Update()
    {

    }

    void FixedUpdate()
    {
        if(targetT != null) 
        {
            Vector3 targetPos = new Vector3(targetT.position.x, targetT.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, camSpd * Time.deltaTime);
        }

    }

    public void SetTarget(GameObject target)
    {
        targetT = target.transform;
    }

    public void CamShake(int strength)
    {
        if(strength <= 1) camAni.SetTrigger("camShake");
        if(strength > 1) camAni.SetTrigger("camShakeHeavy");
    }


}
