using System;
using UnityEngine;

public class HouseDetecter : MonoBehaviour
{
    public event Action onHouseEnter; // Event triggered when the player enters a house
    public event Action onHouseExit; // Event triggered when the player exits a house

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Floor"))
            onHouseEnter?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Floor"))
            onHouseExit?.Invoke();
    }
}