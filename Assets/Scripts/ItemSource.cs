using UnityEngine;

public class ItemSource : MonoBehaviour
{
    [SerializeField] private ItemType itemType; // Type of the item
    [SerializeField] private int itemAmount; // Item amount
    [SerializeField] private Sprite sourceSprite; // Item sprite
    [SerializeField] private ItemManager itemManager; // Reference to the item manager
    [SerializeField] private PlayerTools playerTools; // Reference to the player tools
    [SerializeField] private Dialogue dialogue; // Reference to the dialogue system2
    [SerializeField] private SourceHp treeHp; // Reference to the tree health script

    public void Awake()
    {
        treeHp.OnSourceDestroyed += () => itemManager.AddItem(itemType, itemAmount); // Subscribe to the tree destroyed event
    }

    public void Interact() {
        if(itemManager.HasItem(ItemType.Axe))
            playerTools.PlayCutAnimation(treeHp); // Play the cutting animation
        else
            dialogue.StartDialogue(gameObject, new string[] {"..."}, sourceSprite); // Start the dialogue with the item sprite
    }
}
