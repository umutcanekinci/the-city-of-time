using System;
using System.Collections;
using UnityEngine;


public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator; // Reference to the Animator component
    [SerializeField] private PlayerTools playerTools; // Reference to the PlayerTools script
    [SerializeField] private FishingMode fishingMode;

    private void Awake()
    {
        fishingMode.onFishingEnd += () => Play("idle");
    }

    private void OnDestroy()
    {
        fishingMode.onFishingEnd -= () => Play("idle"); // Unsubscribe from the event to avoid memory leaks
    }

    public bool IsFinished(string animationName)
    {
        // Check if the animation is playing and has completed at least one full cycle
        return IsPlaying(animationName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f;
    }

    public bool IsPlaying(string animationName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName); // Check if the specified animation is currently playing
    }

    public void Play(string animationName)
    {
        animator.Play(animationName); // Play the specified animation
    }

    public void PlayAnimationWithEnd(string animationName, Action onEnd)
    {
        animator.Play(animationName); // Play the specified animation
        StartCoroutine(WaitForAnimationEnd(animationName, onEnd)); // Start a coroutine to wait for the animation to finish
    }

    private IEnumerator WaitForAnimationEnd(string animationName, Action onEnd)
    {
        while (!IsFinished(animationName)) // Wait until the animation is finished
        {
            yield return null; // Wait for the next frame
        }
        onEnd?.Invoke(); // Invoke the callback action when the animation is finished
    }
}