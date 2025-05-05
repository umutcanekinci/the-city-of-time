using System;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText; // Reference to the TextMeshProUGUI component for displaying the countdown
    [SerializeField] private float countdownTime = 5f; // Time in seconds for the countdown
    [SerializeField] private event Action onCountdownComplete; // Event to notify when the countdown is complete
    private float currentTime; // Variable to keep track of the current countdown time
    private bool isCountingDown = false; // Flag to check if the countdown is active
    

    private void Start()
    {
        countdownText = GetComponent<TextMeshProUGUI>(); // Get the TextMeshProUGUI component attached to this GameObject
        currentTime = countdownTime; // Initialize the current time to the countdown time
        countdownText.text = ""; // Clear the text at the start
    }

    private void Update()
    {
        if (isCountingDown)
        {
            currentTime -= Time.deltaTime; // Decrease the current time by the time elapsed since the last frame
            countdownText.text = Mathf.Ceil(currentTime).ToString(); // Update the countdown text with the remaining time

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
