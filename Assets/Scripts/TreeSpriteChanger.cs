using UnityEngine;

public class TreeSpriteChanger : MonoBehaviour
{
    [SerializeField] private GameObject cuttedTreePrefab; // Prefab of the cutted tree sprite
    [SerializeField] private SourceHp sourceHp; // Reference to the SourceHp script
    
    [SerializeField] private Animator leavesAnimator; // Reference to the leaves animator
    private void Awake()
    {
        sourceHp.OnSourceDestroyed += OnTreeDestroyed; // Subscribe to the tree destroyed event
        sourceHp.OnHit += OnHit; // Subscribe to the tree hit event
    }

    private void OnDestroy()
    {
        sourceHp.OnSourceDestroyed -= OnTreeDestroyed; // Unsubscribe from the event to avoid memory leaks
        sourceHp.OnHit -= OnHit; // Unsubscribe from the event to avoid memory leaks
    }

    private void OnHit()
    {  
        leavesAnimator.SetTrigger("Hit"); // Trigger the hit animation on the leaves
    }

    private void OnTreeDestroyed()
    {
        GameObject cuttedTree = Instantiate(cuttedTreePrefab, transform.position, Quaternion.identity); // Instantiate the cutted tree prefab at the current position
        Vector3 newPosition = cuttedTree.transform.position;
        newPosition.y -= 0.7270002f;
        cuttedTree.transform.position = newPosition;        
    }
}
