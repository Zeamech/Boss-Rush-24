using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingTable : MonoBehaviour
{
    [SerializeField] private Transform itemSlot;
    [SerializeField] private bool triggerActive = false;

    private void Awake()
    {
        itemSlot = transform.Find("Item Slot");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            triggerActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            triggerActive = false;
        }
    }

    private void Update()
    {
        if (triggerActive && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Collision");
        }
    }
}
