using UnityEngine;


public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator; // Reference to the Animator component
    [SerializeField] private PlayerTools playerTools; // Reference to the PlayerTools script
    [SerializeField] private FishingMode fishingMode;

    private void Awake()
    {
        fishingMode.onFishingStart += () => Play("casting"); // Subscribe to the event to play casting animation
        fishingMode.onFishingEnd += () => Play("idle"); // Subscribe to the onFishingEnd event to enable cut animation
    }

    private void OnDestroy()
    {
        fishingMode.onFishingStart -= () => Play("casting"); // Unsubscribe from the event to avoid memory leaks
        fishingMode.onFishingEnd -= () => Play("idle"); // Unsubscribe from the event to avoid memory leaks
    }

    public bool IsFinished(string animationName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.70f; // Check if the animation is finished
    }
    private void Update()
    {
    }

    public void Play(string animationName)
    {
        animator.Play(animationName); // Play the specified animation
    }
}