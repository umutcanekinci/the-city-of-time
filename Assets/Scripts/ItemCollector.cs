using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private ItemManager itemManager; // Reference to the item manager

    public void OnTriggerEnter2D(Collider2D other) // Method to handle item collection when the player enters the trigger area
    {
        if (other.CompareTag("Item")) // Check if the collided object has the "Item" tag
        {
            Item item = other.GetComponent<Item>(); // Get the Item component from the collided object
            if (item == null) return;
        
            itemManager.AddItem(item.ItemType, item.ItemAmount); // Add the item to the inventory using the ItemManager
            Destroy(other.gameObject); // Destroy the collected item object
        }
    }
}