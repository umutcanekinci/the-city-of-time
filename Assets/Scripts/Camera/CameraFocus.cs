using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        gameManager.onGameStart += FocusTarget; // Subscribe to the game start event
    }

    private void OnDestroy()
    {
        gameManager.onGameStart -= FocusTarget; // Unsubscribe from the game start event
    }

    private void FocusTarget()
    {
        if (target == null)
            return;
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z); // Set the camera position to the target's position
    }

}
