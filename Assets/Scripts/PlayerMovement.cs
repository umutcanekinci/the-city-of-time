using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; // Reference to the Rigidbody component
    [SerializeField] private Animator animator; // Reference to the Animator component
    [SerializeField] private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    [SerializeField] private float speed = 5f; // Speed of the player movement

    private float horizontal; // Horizontal input value
    private float vertical; // Vertical input value
    private Vector2 direction = Vector2.zero; // Direction of movement
    public event Action onPlayerMove; // Event to notify when the player moves
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); // Get horizontal input (A/D or Left/Right arrow keys)
        vertical = Input.GetAxisRaw("Vertical"); // Get vertical input (W/S or Up/Down arrow keys)
        direction.x = horizontal; // Set the x direction based on horizontal input
        direction.y = vertical; // Set the y direction based on vertical input
        direction.Normalize(); // Normalize the direction vector to ensure consistent movement speed
    }

    private bool isWalking() {
        // Check if the player is moving horizontally or vertically
        return horizontal != 0 || vertical != 0;
    }

    private void FixedUpdate()
    {
        animator.SetBool("isWalking", isWalking()); // Set the walking animation based on movement

        Move();
        Flip(); // Flip the player sprite based on movement direction
    }

    private void Move()
    {
        if (isWalking()) // If the player is moving
        {
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime); // Move the player     
            onPlayerMove?.Invoke(); // Invoke the player move event
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // Stop horizontal movement while maintaining vertical velocity
        }
    }

    private void Flip() {
        if(!isWalking()) return; // If not walking, do not flip

        // Flip the player sprite based on the direction of movement
        spriteRenderer.flipX = direction.x < 0; // Flip the sprite if moving left
    }
}
