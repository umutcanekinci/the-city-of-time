
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint; // Reference to the spawn point
    [SerializeField] private PlayerInteraction playerInteraction; // Reference to the PlayerInteraction script
    [SerializeField] private Item item; // Reference to the InventoryItem script

    public void Spawn()
    {
        for (int i = 0; i < item.ItemAmount; i++)
        {
            Item itemObject = Instantiate(item, transform.position, Quaternion.identity); // Instantiate the item at the current position
            // Position should be in a radius of 0.5f from the source with a random position
            itemObject.transform.position = new Vector3(spawnPoint.position.x + Random.Range(-0.5f, 0.5f), spawnPoint.position.y - 0.25f + Random.Range(-0.5f, 0.5f), spawnPoint.position.z); // Adjust the position of the item
            itemObject.SetItemAmount(1); // Set the item amount to 1
        }
        playerInteraction.EnableInteraction(); // Enable interaction after spawning the item
    }
}
