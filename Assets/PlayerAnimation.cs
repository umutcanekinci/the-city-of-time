using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator; // Reference to the Animator component
    private TreeHp targetTreeHp; // Reference to the TreeHp script
    [SerializeField] private int damage = 10; // Damage amount to be dealt to the tree
    private bool hasDealtDamage = false; // Flag to ensure damage is dealt only once per animation

    public void Update()
    {
        if (!isCutting()) return;

        if (isAnimationFinished() && !hasDealtDamage)
        {
            targetTreeHp.LoseHp(damage); // Deal damage to the tree
            hasDealtDamage = true; // Mark that damage has been dealt
        }
    }

    public bool isCutting()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("axe");
    }

    public bool isAnimationFinished()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f;
    }

    public void PlayCutAnimation(TreeHp treeHp)
    {
        animator.Play("axe");
        targetTreeHp = treeHp; // Store the reference to the TreeHp script
        hasDealtDamage = false; // Reset the flag when a new animation starts
    }
}
