using UnityEngine;

public class PlayerTools : MonoBehaviour
{
    [SerializeField] private Animator animator; // Reference to the Animator component
    private SourceHp targetTreeHp; // Reference to the TreeHp script
    [SerializeField] private int damage = 10; // Damage amount to be dealt to the tree
    private bool hasDealtDamage = false; // Flag to ensure damage is dealt only once per animation

    public void Update()
    {
        if (!isCutting()) return;

        if (IsAnimationFinished() && !hasDealtDamage)
        {
            targetTreeHp.LoseHp(damage); // Deal damage to the tree
            hasDealtDamage = true; // Mark that damage has been dealt
        }
    }

    public bool isCutting()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("axe");
    }

    public bool IsAnimationFinished()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f;
    }

    public void PlayCutAnimation(SourceHp treeHp)
    {
        animator.Play("axe");
        targetTreeHp = treeHp; // Store the reference to the TreeHp script
        hasDealtDamage = false; // Reset the flag when a new animation starts
    }
}
