using System;
using UnityEngine;

public enum ItemType
{
    Resource,
    Equipable,
    Consumable
}

// 소모 아이템 사용 시 변경될 Conditions
public enum ConsumableType
{
    Hunger,
    Health
}

[Serializable]
public class ItemDataConsumable
{
    public ConsumableType tyep;
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class itemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPerfab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    //소모 가능한 아이템의 체력 데이터
    [Header("Consumable")]
    public ItemDataConsumable[] consumables;
}

