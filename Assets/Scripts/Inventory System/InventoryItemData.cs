using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    public enum Type
    {
        Weapon,
        Armor,
        Merch,
        Talisman
    }
    public int ID;
    public string DisplayName;
    [TextArea(4, 4)]
    public string Description;
    public Type ItemType;
    public Sprite Icon;
    public int MaxStackSize;

    public float attackCoolsown = 0.3f;
    public AttackBehaviour BasicAttack;
    public AttackBehaviour SlideAttack;
    public AttackBehaviour SprintAttack;
    public AttackBehaviour BlockAttack;
    public AttackBehaviour DrawAttack;

}
