using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public List<string> collectedItems;
    public SerializableDictionary<string, ItemPickupSaveData> activeItems;

    public SerializableDictionary<string, InventorySaveData> chestDictionary;
    public SerializableDictionary<string, ShopSaveData> _shopKeeperDictionary;

    public InventorySaveData playerBackpack;
    public InventorySaveData playerArmor;
    public InventorySaveData playerTalisman;
    public InventorySaveData playerWeapons;

    public SaveData()
    {
        collectedItems = new List<string>();
        activeItems = new SerializableDictionary<string, ItemPickupSaveData>();
        chestDictionary = new SerializableDictionary<string, InventorySaveData>();
        playerBackpack = new InventorySaveData ();
        playerArmor = new InventorySaveData ();
        playerTalisman = new InventorySaveData ();
        playerWeapons = new InventorySaveData ();
        _shopKeeperDictionary = new SerializableDictionary<string, ShopSaveData>();
    }
}
