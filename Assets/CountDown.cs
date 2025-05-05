using System;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText; // Reference to the TextMeshProUGUI component for displaying the countdown
    [SerializeField] private float countdownTime = 5f; // Time in seconds for the countdown
    [SerializeField] private event Action onCountdownComplete; // Event to notify when the countdown is complete
    [SerializeField] private bool isCountingDown = true; // Flag to check if the countdown is active
    private float currentTime; // Variable to keep track of the current countdown time
    
    private void Start()
    {
        currentTime = countdownTime; // Initialize the current time to the countdown time
    }

    private void Update()
    {
        if (isCountingDown)
        {
            // Text format: "00:00" (minutes:seconds)
            countdownText.text = string.Format("{0:00}:{1:00}", Mathf.Floor(currentTime / 60), Mathf.Floor(currentTime % 60)); // Update the text with the current time in minutes and seconds
            currentTime -= Time.deltaTime; // Decrease the current time by the time elapsed since the last frame

            if (currentTime < 1f) // Check if the countdown has gone below zero
            {
                countdownText.color = Color.red; // Change the text color to red
            }

            if (currentTime <= 0f) // Check if the countdown has reached zero
            {
                isCountingDown = false; // Stop the countdown
                countdownText.text = "0"; // Set the text to zero
                onCountdownComplete(); // Call the method to handle countdown completion
            }
        }
    }

    public void StartCountdown()
    {
        isCountingDown = true; // Start the countdown
        currentTime = countdownTime; // Reset the current time to the countdown time
    }
}
