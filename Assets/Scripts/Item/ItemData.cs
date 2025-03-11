using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    Equippable,
    Consumable,
    Resource
}

public enum ConsumableType
{
    Hunger,
    Health,
    Stamina,
    Boost
}

public enum BoostType
{
    RegenerationHealth,
    RegenerationHunger,
    RegenerationStamina,

    
}

[CreateAssetMenu(fileName ="Item", menuName ="New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking Info")]
    public bool isStackable;
    public int maxStackCount;

    //[Header("Consumable Info")]
    public ItemDataConsumable[] consumables;

    //[Header("Boosting Info")]
    public ItemDataBoostable[] boosts;
}

[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

[System.Serializable]
public class ItemDataBoostable
{
    public BoostType type;
    public float duration;
    public float interval;
}
