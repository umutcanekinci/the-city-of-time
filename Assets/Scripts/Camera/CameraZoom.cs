using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public enum CameraZoomState
    {
        ZoomIn,
        ZoomOut,
        None
    }


    private float minZoom = 3f; // Minimum zoom level
    private float maxZoom = 6f; // Maximum zoom level
    [SerializeField] private CountDown countDown; // Reference to the countdown timer
    [SerializeField] private HouseDetecter houseDetecter; // Reference to the house detector
    [SerializeField] private Spawner playerSpawner; // Reference to the player spawner
    [SerializeField] private Camera mainCamera; // Reference to the camera
    [SerializeField] private float zoomSpeed = 0.1f; // Speed of zooming in and out
    [SerializeField] private float currentZoom;
    [SerializeField] private float targetZoom;
    [SerializeField] private float inHouseZoom = 2.5f;
    [SerializeField] private float deathZoom = 20f; // Zoom level when the player dies
    [SerializeField] private float outHouseZoom = 5f; // Zoom level when outside the house
    [SerializeField] private CameraZoomState currentZoomState = CameraZoomState.None; // Current zoom state
    
    public void Awake()
    {
        countDown.onCountdownEnd += () => SmoothZoom(deathZoom); // Subscribe to the countdown end event
        playerSpawner.onSpawned += () => SmoothZoom(inHouseZoom); // Subscribe to the spawn event
        houseDetecter.onHouseEnter += () => SmoothZoom(inHouseZoom); // Subscribe to the house enter event
        houseDetecter.onHouseExit += () => SmoothZoom(outHouseZoom); // Subscribe to the house exit event
    }

    private void OnDestroy()
    {
        countDown.onCountdownEnd -= () => SmoothZoom(deathZoom); // Unsubscribe from the countdown end event
        playerSpawner.onSpawned -= () => SmoothZoom(inHouseZoom); // Unsubscribe from the spawn event
        houseDetecter.onHouseEnter += () => SmoothZoom(inHouseZoom); // Subscribe to the house enter event
        houseDetecter.onHouseExit += () => SmoothZoom(outHouseZoom); // Subscribe to the house exit event
    }

    public void SmoothZoom(float finalZoom)
    {
        SmoothZoom(currentZoom, finalZoom); // Smoothly zoom the camera to a specific zoom level
    }

    public void SmoothZoom(float startZoom, float finalZoom)
    {
        SetZoom(startZoom); // Set the camera to a specific zoom level
        SetState(startZoom < finalZoom ? CameraZoomState.ZoomIn : CameraZoomState.ZoomOut); // Set the zoom state based on the start and final zoom levels
        minZoom = Mathf.Min(startZoom, finalZoom); 
        maxZoom = Mathf.Max(startZoom, finalZoom);
    }

    public void SetState(CameraZoomState state)
    {
        currentZoomState = state; // Set the current zoom state
    }

    private void Update()
    {
        HandleZoom(); // Call the HandleZoom method every frame
    }

    private void HandleZoom()
    {
        if(currentZoomState == CameraZoomState.None)
            return;

        currentZoom = mainCamera.orthographicSize;
        targetZoom = currentZoomState == CameraZoomState.ZoomIn ? maxZoom : minZoom;

        if (Mathf.Abs(currentZoom - targetZoom) < 0.1f) {
            SetState(CameraZoomState.None);
            currentZoom = targetZoom;
            return;
        }
            
        mainCamera.orthographicSize = Mathf.Lerp(currentZoom, targetZoom, zoomSpeed * Time.deltaTime);
    }

    public void SetZoom(float zoom) {
        mainCamera.orthographicSize = zoom; // Set the camera to a specific zoom level
    }

}