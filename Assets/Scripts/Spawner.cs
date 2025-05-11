using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint; // Reference to the spawn point
    public event Action onSpawned; // Event triggered when the player is spawned

    public void Start() // Start method to initialize the spawn point
    {
        if(spawnPoint == null) // Check if the spawn point is not set
            spawnPoint = transform; // Set the spawn point to the current transform
    }

    public void Spawn() // Method to spawn an item at the spawn point
    {
        gameObject.SetActive(true);
        transform.position = spawnPoint.position;
        onSpawned?.Invoke();
    }
}