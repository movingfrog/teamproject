using System;
using UnityEngine;

public enum ItemType
{
    Resource,
    Equipable,
    Consumable
}

// �Ҹ� ������ ��� �� ����� Conditions
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

    //�Ҹ� ������ �������� ü�� ������
    [Header("Consumable")]
    public ItemDataConsumable[] consumables;
}

