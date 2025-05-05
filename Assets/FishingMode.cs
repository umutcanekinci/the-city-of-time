using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FishingMode : MonoBehaviour
{
    [SerializeField] private GameObject fishingUI; // Reference to the fishing UI
    [SerializeField] private Camera mainCamera; // Reference to the main camera
    [SerializeField] private Transform player; // Reference to the player transform]
    [SerializeField] private bool isFishingModeStarted = false; // Flag to check if the player is fishing
    [SerializeField] private PlayerAnimation playerAnimation; // Reference to the Animator component
    [SerializeField] private CameraZoom cameraZoom; // Reference to the CameraZoom script
    [SerializeField] private Slider slider;
    [SerializeField] private RectTransform successBar; // Reference to the success bar UI element
    [SerializeField] private Item fishItemPrefab; // Reference to the fish item prefab
    public event Action onFishingEnd; // Event to notify when fishing ends

    public void BeginStarting()
    {
        playerAnimation.PlayAnimationWithEnd("casting", Activate); // Play the waiting animation
        cameraZoom.SetState(CameraZoom.CameraZoomState.ZoomIn);
    }

    public void BeginStopping()
    {
        playerAnimation.PlayAnimationWithEnd("reeling", () => playerAnimation.PlayAnimationWithEnd("caught", onFishingEnd)); // Play the casting animation again to stop fishing
        cameraZoom.SetState(CameraZoom.CameraZoomState.ZoomOut);
    }

    public void Activate()
    {
        fishingUI.SetActive(true); // Activate the fishing UI
        isFishingModeStarted = true; // Set the fishing flag to true
    }

    public void Disable()
    {
        fishingUI.SetActive(false); // Deactivate the fishing UI
        isFishingModeStarted = false; // Set the fishing flag to false
    }

    public void Update()
    {
        if (isFishingModeStarted) {
            if(playerAnimation.IsPlaying("waiting"))
                slider.value = Mathf.PingPong(Time.time, 1); // Update the slider value for the success bar
        
            if (Input.GetKeyDown(KeyCode.E)) {
                    BeginStopping(); // Stop fishing when E is pressed
                    Disable(); // Disable the fishing UI
            }

            if (Input.GetKeyDown(KeyCode.F)) // Check if the player is fishing and presses the F key
            {
                // Handle the fishing action here (e.g., catch a fish)
                playerAnimation.PlayAnimationWithEnd("reeling", () => playerAnimation.PlayAnimationWithEnd("caught", OnCaught)); // Play the reeling animation
            }
        }
    }

    public void OnCaught() {   
        // Check if the slider value is within the success range then instantiate the fish
        if (slider.value >= 0.5f) {
            Item fishItem = Instantiate(fishItemPrefab, player.position, Quaternion.identity); // Instantiate the fish item prefab at the player's position
            fishItem.gameObject.SetActive(true); // Activate the fish item
        } else {
            Debug.Log("Missed!"); // Log a message if the player missed
        }
        playerAnimation.Play("waiting"); // Play the waiting animation again
    }

}
