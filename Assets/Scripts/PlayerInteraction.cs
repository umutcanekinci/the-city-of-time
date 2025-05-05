using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    
    [SerializeField] private Dialogue dialogue; // Reference to the Dialogue script
    [SerializeField] private FishingMode fishingMode; // Reference to the FishingMode script
    [SerializeField] private PlayerMovement playerMovement; // Reference to the PlayerMovement script
    private bool isInInteraction = false; // Flag to check if the player is in interaction
    private ArrayList objectsInRange; // Array to store objects in range
    private Interaction closestInteractable;
    private Interaction interact;
    
    public void Awake()
    {
        dialogue.onDialogueEnd   += (gameObject) => { EnableInteraction(); }; // Subscribe to the OnDialogueEnd event to reset the interaction flag
        fishingMode.onFishingEnd += EnableInteraction; // Subscribe to the onFishingEnd event to reset the interaction flag
    }

    public void OnDestroy()
    {
        dialogue.onDialogueEnd -= (gameObject) => { EnableInteraction(); }; // Unsubscribe from the event to avoid memory leaks
        fishingMode.onFishingEnd -= EnableInteraction; // Unsubscribe from the event to avoid memory leaks
    }

    public void EnableInteraction()
    {
        isInInteraction = false; // Reset the interaction flag
        playerMovement.enabled = false; // Disable player movement
    }

    public void DisableInteraction()
    {
        isInInteraction = true; // Set the interaction flag to true
        playerMovement.enabled = true; // Enable player movement
    }

    private void Start()
    {
        objectsInRange = new ArrayList(); // Initialize the array
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        interact = other.GetComponent<Interaction>(); // Get the Interact component from the collided object
        if (interact == null) return; // If the object doesn't have an Interact component, exit the method
        if (objectsInRange.Contains(interact)) return; // If the object is already in the array, exit the method
        
        objectsInRange.Add(interact);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        interact = other.GetComponent<Interaction>(); // Get the Interact component from the collided object
        if (interact == null) return; // If the object doesn't have an Interact component, exit the method
        if (!objectsInRange.Contains(interact)) return; // If the object is not in the array, exit the method
        
        interact.GetComponent<NameText>().Highlight(false);
        objectsInRange.Remove(interact); // Remove the object from the array
    }

    private void Update()
    {
        if(isInInteraction) return; // If the player is already in interaction, exit the method

        if (objectsInRange.Count == 0) return;

        closestInteractable = GetClosestInteractable(); // Get the closest interactable object
        if (closestInteractable == null) return; // If there is no closest interactable object, exit the method

        foreach (Interaction interact in objectsInRange) // Loop through all objects in range
            interact.GetComponent<NameText>().Highlight(interact == closestInteractable);

        if (Input.GetKeyDown(KeyCode.E)) {
            closestInteractable.Interact(); // Start the dialogue with the closest interactable object
            isInInteraction = true; // Set the interaction flag to true
        }
    }

    private Interaction GetClosestInteractable()
    {
        closestInteractable = null; // Variable to store the closest interactable object
        float closestDistance = Mathf.Infinity; // Initialize the closest distance to infinity

        foreach (Interaction interact in objectsInRange) // Loop through all objects in range
        {
            float distance = Vector3.Distance(transform.position, interact.transform.position); // Calculate the distance to the object
            if (distance < closestDistance) // If the distance is less than the closest distance
            {
                closestDistance = distance; // Update the closest distance
                closestInteractable = interact; // Update the closest interactable object
            }
        }

        return closestInteractable; // Return the closest interactable object
    }
}
