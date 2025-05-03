using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue; // Reference to the dialogue object
    [SerializeField] private Transform player;
    [SerializeField] private bool isInRange;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entered the trigger collider"); // Log when the player enters the trigger collider
        if (other.CompareTag("Player")) // Check if the object that entered the trigger collider has the tag "Player"
        {
            Debug.Log("Player is in range"); // Log when the player is in range
        }
        else
        {
            Debug.Log("Player is not in range"); // Log when the player is not in range
        }
        if (!other.CompareTag("Player")) return;
        isInRange = true; // Set the flag to true when the player enters the trigger collider
    }

    private void OnTriggerExit(Collider other)
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
