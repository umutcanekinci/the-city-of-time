using System;
using UnityEngine;

public enum ItemType // Enum to represent different types of items
{
    Wood,
    Fish,
    Axe,
}

[Serializable]
public class TaskItem
{
    [SerializeField] private ItemType itemType; // Type of the item
    [SerializeField] private int requiredAmount; // Target amount to collect
    [SerializeField] private int collectedAmount; // Amount to collect


    public ItemType ItemType => itemType; // Property to get the item type
    public int RequiredAmount => requiredAmount; // Property to get the target amount
    public int CollectedAmount => collectedAmount; // Property to get the collected amount

    public void AddCollectedAmount(int amount) // Method to add to the collected amount
    {
        collectedAmount += amount;
    }
}

[Serializable]
public class InventoryItem // Class to represent an item in the inventory
{
    [SerializeField] private ItemType itemType; // Type of the item
    [SerializeField] private int itemAmount; // Item amount
    [SerializeField] private Sprite itemSprite; // Item sprite

    public ItemType ItemType => itemType; // Property to get the item type
    public int ItemAmount => itemAmount; // Property to get the item amount
    public Sprite ItemSprite => itemSprite; // Property to get the item sprite

    public void SetItemAmount(int amount) // Method to set the item amount
    {
        itemAmount = amount;
    }

    public void AddItemAmount(int amount) // Method to add to the item amount
    {
        itemAmount += amount;
    }
}

public class Item : MonoBehaviour
{
    [SerializeField] private ItemType itemType; // Type of the item
    [SerializeField] private int itemAmount; // Item amount
    [SerializeField] private Sprite itemSprite; // Item sprite

    public ItemType ItemType => itemType; // Property to get the item type
    public int ItemAmount => itemAmount; // Property to get the item amount
    public Sprite ItemSprite => itemSprite; // Property to get the item sprite

    public void SetItemAmount(int amount) // Method to set the item amount
    {
        itemAmount = amount;
    }
}
