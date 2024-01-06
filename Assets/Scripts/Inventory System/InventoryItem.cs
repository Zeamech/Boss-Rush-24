using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{ 
    public Image image;
    
    [HideInInspector] public Transform ParentAfterDrag;
    [HideInInspector] public ItemsSO Item;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void InitializeItem(ItemsSO newItem)
    {
        Item = newItem;
        image.sprite = newItem.Icon;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        ParentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(ParentAfterDrag);
    }
}
