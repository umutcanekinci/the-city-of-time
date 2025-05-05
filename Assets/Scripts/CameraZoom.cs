using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public enum CameraZoomState
    {
        ZoomIn,
        ZoomOut,
        None
    }

    [SerializeField] private Camera mainCamera; // Reference to the camera
    [SerializeField] private float zoomSpeed = 0.1f; // Speed of zooming in and out
    [SerializeField] private float minZoom = 5f; // Minimum zoom level
    [SerializeField] private float maxZoom = 15f; // Maximum zoom level
    private CameraZoomState currentZoomState = CameraZoomState.None; // Current zoom state
    public bool IsZoomingIn => currentZoomState == CameraZoomState.ZoomIn; // Check if zooming in
    public bool IsZoomingOut => currentZoomState == CameraZoomState.ZoomOut; // Check if zooming out
    public bool IsZooming => currentZoomState != CameraZoomState.None; // Check if zooming

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
        if (currentZoomState == CameraZoomState.ZoomIn)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, minZoom, zoomSpeed * Time.deltaTime); // Zoom in
            if (Mathf.Abs(mainCamera.orthographicSize - minZoom) < 0.01f) // Check if the zoom level is close to the minimum
            {
                SetState(CameraZoomState.None); // Reset the zoom state
            }
        }
        else if (currentZoomState == CameraZoomState.ZoomOut)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, maxZoom, zoomSpeed * Time.deltaTime); // Zoom out
            if (Mathf.Abs(mainCamera.orthographicSize - maxZoom) < 0.01f) // Check if the zoom level is close to the maximum
            {
                SetState(CameraZoomState.None); // Reset the zoom state
            }
        }
    }

    public void Maximize() {
        mainCamera.orthographicSize = maxZoom; // Set the camera to maximum zoom level
    }

    public void Minimize() {
        mainCamera.orthographicSize = minZoom; // Set the camera to minimum zoom level
    }    
}