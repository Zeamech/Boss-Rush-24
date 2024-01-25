using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MouseTooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemTooltipName;
    [SerializeField] private TextMeshProUGUI itemTootipDescription;
    [SerializeField] private GameObject currentMouseObject;

    private MouseItemData _mouseItemData;

    private void OnEnable()
    {
        InventorySlot_UI.MouseHoveredOverInventoryItem += ShowTooltip;
    }

    private void OnDisable()
    {
        InventorySlot_UI.MouseHoveredOverInventoryItem -= ShowTooltip;
    }

    private void Awake()
    {
        itemTooltipName.transform.parent.gameObject.SetActive(false);
        _mouseItemData = currentMouseObject.GetComponent<MouseItemData>();
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }

    private void ShowTooltip(InventorySlot slot, bool setactive)
    {
        var data = slot.ItemData;

        if (slot.ItemData != null && setactive == true)
        {
            itemTooltipName.transform.parent.gameObject.SetActive(true);
            itemTooltipName.text = data.DisplayName;
            itemTootipDescription.text = data.Description;
            Debug.Log(data.DisplayName.ToString());
        }
        else itemTooltipName.transform.parent.gameObject.SetActive(false);
    }
}
