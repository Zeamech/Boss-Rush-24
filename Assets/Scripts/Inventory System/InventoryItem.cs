using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{ 
    public Image Image;
    public Text CountText;
    
    [HideInInspector] public Transform ParentAfterDrag;
    [HideInInspector] public int Count = 1;
    [HideInInspector] public ItemsSO Item;

    public void InitializeItem(ItemsSO newItem)
    {
        Image = GetComponent<Image>();
        Item = newItem;
        Image.sprite = newItem.Icon;
        RefreshCount();
    }

    public void RefreshCount()
    {
        CountText.text = Count.ToString();
        bool textActive = Count > 1;
        CountText.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Image.raycastTarget = false;
        ParentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Image.raycastTarget = true;
        transform.SetParent(ParentAfterDrag);
    }
}
