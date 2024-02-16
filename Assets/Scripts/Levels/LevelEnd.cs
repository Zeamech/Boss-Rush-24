using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    public GameObject blockage;
    public Transform startPoint;

    LevelManager levelManager;
    private bool endLevel;
    private float endTimer = 3;

    void Start()
    {
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    void Update()
    {
        if(!levelManager.activeBoss.activeSelf)
        {
            blockage.SetActive(false);
        }

        if (endLevel)
        {
            endTimer -= Time.deltaTime;

            if(endTimer <= 0)
            {
                levelManager.NextRoom();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>())
        {
            FindAnyObjectByType<ScreenEffects>().ScreenWipe(true);
            endLevel = true;
        }
        
    }
}
