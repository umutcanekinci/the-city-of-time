using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; // Reference to the Rigidbody component
    [SerializeField] private Animator animator; // Reference to the Animator component
    [SerializeField] private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    [SerializeField] private float speed = 5f; // Speed of the player movement
    [SerializeField] private float jumpForce = 5f; // Force applied when jumping
    [SerializeField] private float gravity = -9.81f; // Gravity force applied to the player

    private float horizontal; // Horizontal input value
    private float vertical; // Vertical input value
    private bool isJumping; // Flag to check if the player is jumping
    private Vector2 direction = Vector2.zero; // Direction of movement
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); // Get horizontal input (A/D or Left/Right arrow keys)
        vertical = Input.GetAxisRaw("Vertical"); // Get vertical input (W/S or Up/Down arrow keys)
        isJumping = Input.GetButtonDown("Jump"); // Check if the jump button is pressed
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
        Jump();
    }

    private void Move()
    {
        if (isWalking()) // If the player is moving
        {
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime); // Move the player
            animator.SetFloat("moveX", direction.x); // Set the x direction for animation
            animator.SetFloat("moveY", direction.y); // Set the y direction for animation
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

    private void Jump()
    {
        if(!isJumping) return; // If not jumping

        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse); // Apply jump force

    }

}
