using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay inventoryPanel;
    public DynamicInventoryDisplay playerInventoryPanel;
    public DynamicInventoryDisplay ArmorPanel;
    public DynamicInventoryDisplay WeaponsPanel;
    public DynamicInventoryDisplay TalismansPanel;

    [SerializeField] private TextMeshProUGUI _goldCount;
    [SerializeField] private GameObject _GoldCountGO;

    private void Awake()
    {
        _GoldCountGO = _goldCount.transform.parent.gameObject;

        TalismansPanel.transform.parent.gameObject.SetActive(false);
        inventoryPanel.gameObject.SetActive(false);
        playerInventoryPanel.transform.parent.gameObject.SetActive(false);
        WeaponsPanel.transform.parent.gameObject.SetActive(false);
        TalismansPanel.transform.parent.gameObject.SetActive(false);
        ArmorPanel.transform.parent.gameObject.SetActive(false);
        _GoldCountGO.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InventorySystem.UpdatePlayerGoldAmount += UpdateGold;
        InventoryHolder.OnDynamicInventoryDisplayReqested += DisplayInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested += DisplayPlayerInventory;
    }

    private void OnDisable()
    {
        InventorySystem.UpdatePlayerGoldAmount -= UpdateGold;
        InventoryHolder.OnDynamicInventoryDisplayReqested -= DisplayInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested -= DisplayPlayerInventory;
    }

    void Update()
    {
        if (inventoryPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame) inventoryPanel.gameObject.SetActive(false);

        if (playerInventoryPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame) playerInventoryPanel.transform.parent.gameObject.SetActive(false);

        if (ArmorPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame) ArmorPanel.transform.parent.gameObject.SetActive(false);

        if (WeaponsPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame) WeaponsPanel.transform.parent.gameObject.SetActive(false);

        if (TalismansPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame) TalismansPanel.transform.parent.gameObject.SetActive(false);
        
        if (_GoldCountGO.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame) _GoldCountGO.gameObject.SetActive(false);
    }

    void DisplayInventory(InventorySystem invToDisplay, int offset)
    {
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(invToDisplay, offset);
    }

    void DisplayPlayerInventory(InventorySystem invToDisplay, int offset, int tab)
    {
        if (tab == 1)
        {
            playerInventoryPanel.transform.parent.gameObject.SetActive(true);
            playerInventoryPanel.RefreshDynamicInventory(invToDisplay, offset);
            _GoldCountGO.gameObject.SetActive(true);
        }
        else if (tab == 2)
        {
            ArmorPanel.transform.parent.gameObject.SetActive(true);
            ArmorPanel.RefreshDynamicInventory(invToDisplay, offset);
        }
        else if (tab == 3)
        {
            WeaponsPanel.transform.parent.gameObject.SetActive(true);
            WeaponsPanel.RefreshDynamicInventory(invToDisplay, offset);
        }
        else if (tab == 4)
        {
            TalismansPanel.transform.parent.gameObject.SetActive(true);
            TalismansPanel.RefreshDynamicInventory(invToDisplay, offset);
        }
    }

    public void UpdateGold(int amount)
    {
        _goldCount.text = amount.ToString();
    }
}
