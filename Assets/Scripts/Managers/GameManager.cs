using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action onGameStart; // Event triggered when the game starts
    [SerializeField] private Spawner playerSpawner; // Reference to the player spawner
    
    private void Start()
    {
        StartGame();
    }

    public void StartGame() {
        onGameStart?.Invoke();
        playerSpawner.Spawn();
    }
}
