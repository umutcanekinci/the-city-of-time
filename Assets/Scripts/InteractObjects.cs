using System.Collections;
using TMPro;
using UnityEngine;

public class InteractObjects : MonoBehaviour
{
    private ArrayList objectsInRange; // Array to store objects in range

    private void Start()
    {
        objectsInRange = new ArrayList(); // Initialize the array
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Interaction interact = other.GetComponent<Interaction>(); // Get the Interact component from the collided object
        if (interact == null) return; // If the object doesn't have an Interact component, exit the method
        if (objectsInRange.Contains(interact)) return; // If the object is already in the array, exit the method
        
        objectsInRange.Add(interact);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Interaction interact = other.GetComponent<Interaction>(); // Get the Interact component from the collided object
        if (interact == null) return; // If the object doesn't have an Interact component, exit the method
        if (!objectsInRange.Contains(interact)) return; // If the object is not in the array, exit the method
        
        TextMeshProUGUI nameText = interact.GetNPC().GetNameText(); // Get the TextMeshProUGUI component for displaying the NPC's name    
        if(nameText.color == Color.yellow) {
            nameText.color = Color.white; // Reset the color of the NPC's name text   
            nameText.text = nameText.text.Replace("<br>Press E to interact", ""); // Remove the interaction prompt from the NPC's name text
        }

        objectsInRange.Remove(interact); // Remove the object from the array
    }

    private void Update()
    {
        if (objectsInRange.Count == 0) return; // If there are no objects in range, exit the method

        Interaction closestInteractable = GetClosestInteractable(); // Get the closest interactable object
        if (closestInteractable == null) return; // If there is no closest interactable object, exit the method

        foreach (Interaction interact in objectsInRange) // Loop through all objects in range
        {
            TextMeshProUGUI nameText = interact.GetNPC().GetNameText(); // Get the TextMeshProUGUI component for displaying the NPC's name    
            if (interact == closestInteractable) // If the object is the closest interactable object
            {
                if(nameText.color == Color.yellow) continue; // If the color is already yellow, exit the method

                nameText.color = Color.yellow;
                nameText.text += "<br>Press E to interact"; // Add the interaction prompt to the NPC's name text

            }
            else // If the object is not the closest interactable object
            {
                if(nameText.color == Color.white) continue; // If the color is already white, exit the method

                nameText.color = Color.white; // Reset the color of the NPC's name text   
                nameText.text = nameText.text.Replace("<br>Press E to interact", ""); // Remove the interaction prompt from the NPC's name text
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) // Check if the "E" key is pressed
        {
            closestInteractable.Interact(); // Start the dialogue with the closest interactable object
        }
    }

    private Interaction GetClosestInteractable()
    {
        Interaction closestInteractable = null; // Variable to store the closest interactable object
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
