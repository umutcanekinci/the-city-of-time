using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager; // Reference to the game manager
    [SerializeField] private InventoryItem[] items; // Array of items in the inventory
    [SerializeField] private TaskManager taskManager; // Reference to the task manager

    private void Awake() // Awake method to initialize the item manager
    {
        gameManager.onGameStart += RemoveAllItems; // Subscribe to the game start event
    }

    private void OnDestroy() // OnDestroy method to unsubscribe from events
    {
        gameManager.onGameStart -= RemoveAllItems; // Unsubscribe from the game start event
    }

    public void AddItem(ItemType itemType, int itemAmount) // Method to add an item to the inventory
    {
        foreach (InventoryItem i in items)
        {
            if (i.ItemType == itemType) // Check if the item already exists in the inventory
            {
                i.AddItemAmount(itemAmount); // Add the item amount to the existing item
                taskManager.UpdateTaskItem(itemType, itemAmount); // Update the task item
                return;
            }
        }
    }

    public bool HasItem(ItemType itemType) // Method to check if the inventory has a specific item
    {
        foreach (InventoryItem i in items)
        {
            if (i.ItemType == itemType && i.ItemAmount > 0) // Check if the item exists and has a positive amount
            {
                return true; // Item exists in the inventory
            }
        }
        return false; // Item does not exist in the inventory
    }

    public void RemoveAllItems() // Method to remove all items from the inventory
    {
        foreach (InventoryItem i in items)
        {
            i.SetItemAmount(0); // Set the item amount to 0 for each item
        }
    }
}
