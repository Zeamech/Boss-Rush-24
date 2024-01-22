using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInventoryHolder : InventoryHolder
{
    [SerializeField] protected InventorySystem ArmorInventorySystem;
    [SerializeField] protected InventorySystem WeaponsInventorySystem;
    [SerializeField] protected InventorySystem TalismansInventorySystem;
    [SerializeField] private int ArmorSlots;
    [SerializeField] private int WeaponSlots;
    [SerializeField] private int TalismanSlots;


    public static UnityAction OnPlayerInventoryChanged;
    public static UnityAction<InventorySystem, int, int> OnPlayerInventoryDisplayRequested;

    private void Start()
    {
        TalismansInventorySystem = new InventorySystem(TalismanSlots);
        ArmorInventorySystem = new InventorySystem(ArmorSlots);
        WeaponsInventorySystem = new InventorySystem(WeaponSlots);
        SaveGameManager.data.playerBackpack = new InventorySaveData(primaryInventorySystem);
        SaveGameManager.data.playerTalisman = new InventorySaveData(TalismansInventorySystem);
        SaveGameManager.data.playerArmor = new InventorySaveData(ArmorInventorySystem);
        SaveGameManager.data.playerWeapons = new InventorySaveData(WeaponsInventorySystem);
    }

    private void OnEnable()
    {
        ChestInventory.OpenPlayerInventory += DisplayPlayerInventory;
    }

    private void OnDisable()
    {
        ChestInventory.OpenPlayerInventory -= DisplayPlayerInventory;
    }

    protected override void LoadInventory(SaveData data)
    {
        //Check the save data for this specific chests inventory, and if it exists, load it in
        if (data.playerBackpack.InvSystem != null)
        {
            this.primaryInventorySystem = data.playerBackpack.InvSystem;
            OnPlayerInventoryChanged?.Invoke();
        }
        if(data.playerTalisman.InvSystem != null)
        {
            this.TalismansInventorySystem = data.playerTalisman.InvSystem;
        }
        if (data.playerArmor.InvSystem != null)
        {
            this.ArmorInventorySystem = data.playerArmor.InvSystem;
        }
        if (data.playerWeapons.InvSystem != null)
        {
            this.WeaponsInventorySystem = data.playerWeapons.InvSystem;
        }
    }

    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame) DisplayPlayerInventory();
    }

    public void DisplayPlayerInventory()
    {
        OnPlayerInventoryDisplayRequested?.Invoke(primaryInventorySystem, offset, 1);
        OnPlayerInventoryDisplayRequested?.Invoke(ArmorInventorySystem, offset, 2);
        OnPlayerInventoryDisplayRequested?.Invoke(WeaponsInventorySystem, offset, 3);
        OnPlayerInventoryDisplayRequested?.Invoke(TalismansInventorySystem, offset, 4);
    }

    public bool AddToInventory(InventoryItemData data, int amount)
    {
        if(primaryInventorySystem.AddToInventory(data, amount))
        {
            return true;
        }
        
        return false;
    }
}
