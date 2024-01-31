using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{

    public bool stunDuration;
    public float LifeTime = 0.3f;

    private List<HealthBar> hitList = new List<HealthBar>();

    [SerializeField] private AK.Wwise.Event playerSwordSlashAudio;

    void Update()
    {
        LifeTime -= Time.deltaTime;
        if(LifeTime <= 0) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.GetComponent<HealthBar>() != null && collision.GetComponent<Player>() == null)
        {
            //for(int i = 0; i < hitList.Count; i++)
            //{
            //    if (hitList[i] == collision.GetComponent<HealthBar>())
            //        break;

            //    if (i + 1 == hitList.Count)
            //    {
            //        //add deal damage here
            //        Debug.Log("Hit", collision.gameObject);
            //        hitList.Add(collision.GetComponent<HealthBar>());
            //    }


            //}

            collision.GetComponent<HealthBar>().AlterHealth(-1);
        }
    }

    public void PlayerSwordSlashAudio()
    {
        playerSwordSlashAudio.Post(gameObject);
    }
}
