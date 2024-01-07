using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TradingTable : MonoBehaviour
{
    public InventorySlot InventorySlot;
    public Button PlayerInventoryButton;
    [SerializeField] private Transform itemPrefab;
    [SerializeField] private bool triggerActive = false;

    private void Awake()
    {
        itemPrefab = transform.Find("Item Slot");
        PlayerInventoryButton = GameObject.FindGameObjectWithTag("PlayerInventoryButton").GetComponent<Button>();
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
            PlayerInventoryButton.onClick.Invoke();
        }

        if (InventorySlot.GetComponentInChildren<InventoryItem>() != null)
        {
            InventoryItem itemInSlot = InventorySlot.GetComponentInChildren<InventoryItem>();
            Instantiate(itemInSlot.Item.Prefab, itemPrefab);
        }
        else
        {
            Debug.Log("No Item In Slot");
        }
    }
}
