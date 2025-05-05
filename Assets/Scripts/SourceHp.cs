using System;
using UnityEngine;

public class SourceHp : MonoBehaviour
{
    [SerializeField] private ItemManager itemManager; // Reference to the item manager
    [SerializeField] private int maxHp = 100; // Maximum health of the tree
    [SerializeField] private int currentHp; // Current health of the tree
    
    public event Action OnHit; // Event to notify when the tree is hit
    public event Action OnSourceDestroyed; // Event to notify when the tree is destroyed

    public void Start()
    {
        currentHp = maxHp; // Initialize current health to maximum health
    }

    public void LoseHp(int amount)
    {
        currentHp -= amount; // Decrease the current health by the specified amount
        OnHit?.Invoke(); // Invoke the hit event to notify that the tree has been hit
        if (currentHp <= 0)
            Destroy(); // Destroy the tree if health is zero or below
    }

    public void Destroy()
    {
        OnSourceDestroyed?.Invoke(); // Invoke the event to notify that the tree is destroyed
        Destroy(gameObject); // Destroy the tree game object
    }
}
