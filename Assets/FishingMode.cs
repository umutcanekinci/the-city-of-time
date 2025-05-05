using System;
using System.Collections;
using UnityEngine;

public class FishingMode : MonoBehaviour
{
    [SerializeField] private GameObject fishingUI; // Reference to the fishing UI
    [SerializeField] private Camera mainCamera; // Reference to the main camera
    [SerializeField] private Transform player; // Reference to the player transform]
    [SerializeField] private bool isFishingModeStarted = false; // Flag to check if the player is fishing
    [SerializeField] private PlayerAnimation playerAnimation; // Reference to the Animator component
    [SerializeField] private CameraZoom cameraZoom; // Reference to the CameraZoom script

    public event Action onFishingStart; // Event to notify when fishing starts
    public event Action onFishingEnd; // Event to notify when fishing ends
    public event Action onReeling; // Event to notify when reeling in a fish

    public void StartFishing()
    {
        onFishingStart?.Invoke(); // Invoke the fishing start event
        cameraZoom.SetState(CameraZoom.CameraZoomState.ZoomIn);
    }

    public void StopFishing()
    {
        playerAnimation.Play("reeling"); // Play the casting animation again to stop fishing
        cameraZoom.SetState(CameraZoom.CameraZoomState.ZoomOut);
    }

    public void EnableFishingUI()
    {
        fishingUI.SetActive(true); // Activate the fishing UI
        isFishingModeStarted = true; // Set the fishing flag to true
    }

    public void DisableFishingUI()
    {
        fishingUI.SetActive(false); // Deactivate the fishing UI
        isFishingModeStarted = false; // Set the fishing flag to false
    }

    public void Update()
    {
        if (!cameraZoom.IsZooming) {
            if (playerAnimation.IsFinished("reeling")) {
                DisableFishingUI(); // Disable the fishing UI after zooming out
                onFishingEnd?.Invoke(); // Invoke the fishing end event
            }
            else if (playerAnimation.IsFinished("casting"))
                EnableFishingUI(); // Enable the fishing UI after zooming in
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            if(isFishingModeStarted) // If the player is not fishing, start fishing
                StopFishing(); // Stop fishing when E is pressed
        }

        if (isFishingModeStarted) {
            if (Input.GetKeyDown(KeyCode.F)) // Check if the player is fishing and presses the F key
            {
                // Handle the fishing action here (e.g., catch a fish)
                onReeling?.Invoke(); // Invoke the reeling event
            }
        }
    }

}
