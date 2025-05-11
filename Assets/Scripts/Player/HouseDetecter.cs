using System;
using UnityEngine;

public class HouseDetecter : MonoBehaviour
{
    [SerializeField] private GameObject roof; // Reference to the roof GameObject
    [SerializeField] private bool isInsideHouse = false; // Flag to track if the player is inside a house 
    public event Action onHouseEnter; // Event triggered when the player enters a house
    public event Action onHouseExit; // Event triggered when the player exits a house
    public bool IsInsideHouse => isInsideHouse; // Public property to check if the player is inside a house
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isInsideHouse)
            return;

        if (other.CompareTag("Floor")) {
            onHouseEnter?.Invoke();
            roof.SetActive(false);
            isInsideHouse = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!isInsideHouse)
            return;

        if (other.CompareTag("Floor")) {
            onHouseExit?.Invoke();
            roof.SetActive(true);
            isInsideHouse = false;
        }
    }
}