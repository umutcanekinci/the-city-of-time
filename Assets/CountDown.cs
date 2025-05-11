using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    [SerializeField] private GameManager gameManager; // Reference to the GameManager script
    [SerializeField] private PlayerMovement playerMovement; // Reference to the PlayerMovement script
    [SerializeField] private PlayerAnimation playerAnimation; // Reference to the PlayerAnimation script
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private TextMeshProUGUI countdownText; // Reference to the TextMeshProUGUI component for displaying the countdown
    [SerializeField] private float countdownTime = 5f; // Time in seconds for the countdown
    [SerializeField] private bool isCountingDown = true; // Flag to check if the countdown is active
    [SerializeField] private float currentTime; // Variable to keep track of the current countdown time
    public event Action onCountdownEnd; // Event triggered when the countdown ends

    private void Awake()
    {
        gameManager.onGameStart += OnGameStart; // Subscribe to the game start event to reset the timer
    }

    private void OnDestroy()
    {
        gameManager.onGameStart -= OnGameStart; // Unsubscribe from the game start event
    }

    private void OnGameStart()
    {
        explosionPrefab.SetActive(false); // Deactivate the explosion prefab
        playerMovement.enabled = true; // Enable player movement
        ResetTimer(); // Reset the timer when the game starts
    }

    public void ResetTimer()
    {
        isCountingDown = true; // Start the countdown
        currentTime = countdownTime; // Reset the current time to the countdown time
        countdownText.color = Color.white; // Reset the text color to white
    }

    private void Update()
    {
        if (isCountingDown)
        {
            // Text format: "00:00" (minutes:seconds)
            countdownText.text = string.Format("{0:00}:{1:00}", Mathf.Floor(currentTime / 60), Mathf.Floor(currentTime % 60)); // Update the text with the current time in minutes and seconds
            currentTime -= Time.deltaTime; // Decrease the current time by the time elapsed since the last frame

            if (currentTime < 10f)
                countdownText.color = Color.red;

            if (currentTime <= 0f) // Check if the countdown has reached zero
            {
                isCountingDown = false; // Stop the countdown
                countdownText.text = "00:00"; // Set the text to zero
                onCountdownEnd?.Invoke();
                StartCoroutine(ExplodeAfterDelay()); // Start the explosion coroutine
            }
        }
    }

    IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second before exploding
        explosionPrefab.SetActive(true); // Activate the explosion prefab
        playerAnimation.PlayAnimationWithEnd("death", Reset);
        playerMovement.enabled = false; // Disable player movement
    }

    public void Reset()
    {
        StartCoroutine(ResetAfterDelay()); // Start the reset coroutine
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second before resetting
        gameManager.StartGame();
    }
}
