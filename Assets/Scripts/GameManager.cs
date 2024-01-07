using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public InventoryManager inventoryManager;
    public ItemsSO[] AllItems;

    private void Awake()
    {
        GameObject.DontDestroyOnLoad(this);

        instance = this;

        //Creates a list of all the items in the game
        AllItems = Resources.LoadAll<ItemsSO>("");
        for (int i = 0; i < AllItems.Length; i++)
        {
            ItemsSO item = AllItems[i];
            item.ID = i;
        }
        instance = this;
    }

    public void PickupItem(int id)
    {
        if(inventoryManager.AllItemsInInventory.Count < inventoryManager.InventorySlots.Count)
        {
            inventoryManager.AddItem(AllItems[id]);
            Debug.Log("Item Added");
        }
        else
        {
            Debug.Log("Item Not Added");
        }
    }
}
