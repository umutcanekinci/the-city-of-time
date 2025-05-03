using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue; // Reference to the dialogue object
    [SerializeField] private Transform player;
    [SerializeField] private bool isInRange;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        isInRange = true; // Set the flag to true when the player enters the trigger collider
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        isInRange = false; // Set the flag to false when the player exits the trigger collider
    }

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E)) // Check if the player is in range and presses the "E" key
        {
            dialogue.StartDialogue(); // Start the dialogue
        }
    }
}
