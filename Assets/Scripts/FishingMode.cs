using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishingMode : MonoBehaviour
{
    [SerializeField] private GameObject fishingUI; // Reference to the fishing UI
    [SerializeField] private Transform player; // Reference to the player transform]
    [SerializeField] private bool isFishingModeStarted = false; // Flag to check if the player is fishing
    [SerializeField] private PlayerAnimation playerAnimation; // Reference to the Animator component
    [SerializeField] private CameraZoom cameraZoom; // Reference to the CameraZoom script
    [SerializeField] private Slider slider;
    [SerializeField] private RectTransform sliderRect; // Reference to the RectTransform of the slider
    [SerializeField] private RectTransform successBar; // Reference to the success bar UI element
    [SerializeField] private Item fishItemPrefab; // Reference to the fish item prefab
    [SerializeField] private float minSuccessBarYScale = 0.5f; // Minimum scale for the success bar
    [SerializeField] private float maxSuccessBarYScale = 1.5f; // Maximum scale for the success bar
    public event Action onFishingEnd; // Event to notify when fishing ends
    [SerializeField] private FadeOutText resultText; // Reference to the result text UI element
    [SerializeField] private static int fishingCount = 0; // Static variable to track the number of fishing attempts
    private void Awake()
    {
        ResetSliderValues();
    }

    public void ResetSliderValues()
    {
        float range = sliderRect.rect.height;
        float radomScale = UnityEngine.Random.Range(minSuccessBarYScale, maxSuccessBarYScale); // Randomize the scale of the success bar
        float randomY = UnityEngine.Random.Range(0, range - successBar.rect.height); // Randomize the Y position of the success bar
        successBar.anchoredPosition = new Vector2(successBar.localPosition.x, -randomY); // Set the position of the success bar
        successBar.localScale = new Vector2(successBar.localScale.x, radomScale); // Set the scale of the success bar
    }

    public void BeginStarting()
    {
        playerAnimation.PlayAnimationWithEnd("casting", Activate); // Play the waiting animation
        cameraZoom.SmoothZoom(cameraZoom.InHouseZoom);
    }

    public void BeginStopping()
    {
        playerAnimation.PlayAnimationWithEnd("reeling", () => playerAnimation.PlayAnimationWithEnd("caught", onFishingEnd)); // Play the casting animation again to stop fishing
        cameraZoom.SmoothZoom(cameraZoom.OutHouseZoom);
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
        UpdateSlider(); // Update the slider value
    }

    public void UpdateSlider()
    {
        if (!isFishingModeStarted)
            return;
        
        if(playerAnimation.IsPlaying("waiting"))
            slider.value = Mathf.PingPong(Time.time * (2 + fishingCount * 0.1f), 1); // Update the slider value for the success bar, increasing speed by fishingCount
    
        if (Input.GetKeyDown(KeyCode.E)) {
            BeginStopping(); // Stop fishing when E is pressed
            Disable(); // Disable the fishing UI
        }

        if (Input.GetKeyDown(KeyCode.F))
            playerAnimation.PlayAnimationWithEnd("reeling", () => playerAnimation.PlayAnimationWithEnd("caught", OnCaught)); // Play the reeling animation
        
    }

    public void OnCaught() {   
        float range = sliderRect.rect.height;
        float succesMinY = -successBar.anchoredPosition.y / range ;
        float succesMaxY = (-successBar.anchoredPosition.y + successBar.rect.height) / range;
        resultText.Reset(); // Reset the result text
        if (slider.value <= succesMaxY && slider.value >= succesMinY) {
            Item fishItem = Instantiate(fishItemPrefab, player.position, Quaternion.identity); // Instantiate the fish item prefab at the player's position
            fishItem.gameObject.SetActive(true); // Activate the fish item

            resultText.SetColor(Color.green); // Set the text color to green
            resultText.SetText("Caught " + fishItem + "!");
            fishingCount++; // Increment the fishing count
        } else {
            resultText.SetColor(Color.red); // Set the text color to red
            resultText.SetText("Missed!"); // Set the text to "Missed!"
        }
        
        resultText.Start();
        ResetSliderValues();
        playerAnimation.Play("waiting"); // Play the waiting animation again
    }

}
