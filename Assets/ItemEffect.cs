using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    private float startHeight;
    [SerializeField] private float speed = 1f; // Speed of the item movement
    [SerializeField] private float distance = 0.1f; // Distance to move the item
    private bool isMovingUp = true; // Flag to check if the item is moving up
    void Start()
    {
        startHeight = transform.position.y; // Store the initial height of the item
    }
    
    void Update()
    {
        Vector3 displacement = speed * Time.deltaTime * Vector3.up; // Calculate the displacement vector for the item movement
        if (isMovingUp)
        {
            transform.position += displacement;
            if (transform.position.y >= startHeight + distance)
                isMovingUp = false;
        }
        else
        {
            transform.position -= displacement;
            if (transform.position.y <= startHeight)
                isMovingUp = true;
        }
    }
}
