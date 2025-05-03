using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    [SerializeField] private Transform target;

    void Start()
    {
        // transform.position = new Vector3(target.position.x, target.position.y + 2, target.position.z - 5);
        // transform.LookAt(target); // Make the camera look at the target
    }

}
