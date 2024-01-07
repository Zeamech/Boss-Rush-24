using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "New Item", fileName = "New Item")]
public class ItemsSO : ScriptableObject
{
    [Header("Only Gameplay")]
    public ItemType Type;
    public int ID;
    public string Name;
    public Sprite Prefab;

    [Header("Only UI")]
    public bool stackable;
    public int StackMax;
    public string Description;

    [Header("Both")]
    public Sprite Icon;
    public int Value;

    public enum ItemType
    {
        Weapon,
        Trade,
        Potion,
        Armor,
        Merchandise,
        Talismans
    }
}
