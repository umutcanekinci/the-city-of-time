using UnityEngine;

public class ItemSource : MonoBehaviour
{
    [SerializeField] private PlayerInteraction playerInteraction; // Reference to the PlayerInteraction script
    [SerializeField] private Item item;
    [SerializeField] private Sprite sourceSprite; // Item sprite
    [SerializeField] private ItemManager itemManager; // Reference to the item manager
    [SerializeField] private PlayerTools playerTools; // Reference to the player tools
    [SerializeField] private Dialogue dialogue; // Reference to the dialogue system2
    [SerializeField] private SourceHp sourceHp; // Reference to the tree health script

    public void Awake()
    {
        //treeHp.OnSourceDestroyed += () => itemManager.AddItem(itemType, itemAmount); // Subscribe to the tree destroyed event
        sourceHp.OnSourceDestroyed += OnSourceDestroyed;
    }

    public void OnDestroy()
    {
        //treeHp.OnSourceDestroyed -= () => itemManager.AddItem(itemType, itemAmount); // Unsubscribe from the event to avoid memory leaks
        sourceHp.OnSourceDestroyed -= OnSourceDestroyed;
    }

    private void OnSourceDestroyed()
    {
        for (int i = 0; i < item.ItemAmount; i++)
        {
            Item itemObject = Instantiate(item, transform.position, Quaternion.identity); // Instantiate the item at the current position
            // Position should be in a radius of 0.5f from the source with a random position
            itemObject.transform.position = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y - 0.25f + Random.Range(-0.5f, 0.5f), transform.position.z); // Adjust the position of the item
            itemObject.SetItemAmount(1); // Set the item amount to 1
        }
        
    }

    public void Interact() {
        playerInteraction.EnableInteraction(); // Enable interaction
        if(itemManager.HasItem(ItemType.Axe))
            playerTools.PlayCutAnimation(sourceHp); // Play the cutting animation
        else
            dialogue.StartDialogue(gameObject, new string[] {"..."}, sourceSprite); // Start the dialogue with the item sprite
    }

    
}
