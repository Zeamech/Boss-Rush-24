using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<InventorySlot> InventorySlots = new List<InventorySlot>();
    public List<ItemsSO> AllItemsInInventory = new List<ItemsSO>();
    public List<ItemsSO> WeaponsAndArmor = new List<ItemsSO>();
    public List<ItemsSO> Merchandise = new List<ItemsSO>();
    public List<ItemsSO> Talisman = new List<ItemsSO>();
    public GameObject inventoryItemPrefab;

    private bool filtering = false;

    private void Awake()
    {
        
    }

    public bool AddItem(ItemsSO item)
    {
        //Check If Any Slot Has Same Item In
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.Item == item && itemInSlot.Count < item.StackMax && itemInSlot.Item.stackable == true)
            {
                itemInSlot.Count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        //Finds Empty Slot
        for (int i = 0; i < InventorySlots.Count; i++)
        {   
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }

    private void SpawnNewItem(ItemsSO item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
        if (!filtering)
        {
            AllItemsInInventory.Add(item);
            switch (item.Type)
            {
                case ItemsSO.ItemType.Weapon: WeaponsAndArmor.Add(item);break;
                case ItemsSO.ItemType.Armor: WeaponsAndArmor.Add(item);break;
                case ItemsSO.ItemType.Merchandise: Merchandise.Add(item);break;
                case ItemsSO.ItemType.Talismans: Talisman.Add(item);break;
            }
        }
    }

    public void ClearInventory()
    {
        for (int i = 0;i < InventorySlots.Count; i++)
        {
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot != null) 
            {
                DestroyImmediate(itemInSlot.gameObject);
            }
        }
    }

    public void AllItemsSelected()
    {
        filtering = true;
        ClearInventory();
        for (int i = 0; i < AllItemsInInventory.Count; i++)
        {
            AddItem(AllItemsInInventory[i]);
        }
        filtering = false;
    }

    public void WeaponsAndArmorSelected()
    {
        filtering = true;
        ClearInventory();
        for (int i = 0; i < WeaponsAndArmor.Count; i++)
        {
            AddItem(WeaponsAndArmor[i]);
        }
        filtering = false;
    }

    public void MerchandiseItems()
    {
        filtering = true;
        ClearInventory();
        for (int i = 0; i < Merchandise.Count; i++)
        {
            AddItem(Merchandise[i]);
        }
        filtering = false;
    }

    public void Talismans()
    {
        filtering = true;
        ClearInventory();
        for (int i = 0; i < Talisman.Count; i++)
        {
            AddItem(Talisman[i]);
        }
        filtering = false;
    }
}
