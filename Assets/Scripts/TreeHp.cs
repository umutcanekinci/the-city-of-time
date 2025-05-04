using UnityEngine;

public class TreeHp : MonoBehaviour
{
    [SerializeField] private int maxHp = 100; // Maximum health of the tree
    [SerializeField] private GameObject cuttedTreePrefab; // Cutted sprite of the tree
    [SerializeField] private int currentHp; // Current health of the tree

    public void Start()
    {
        currentHp = maxHp; // Initialize current health to maximum health
    }

    public void LoseHp(int amount)
    {
        currentHp -= amount; // Decrease the current health by the specified amount
        if (currentHp <= 0)
        {
            DestroyTree(); // Destroy the tree if health is zero or below
        }
    }

    public int GetCurrentHp()
    {
        return currentHp; // Return the current health of the tree
    }

    public void DestroyTree()
    {
        GameObject cuttedObject = Instantiate(cuttedTreePrefab, transform.position, Quaternion.identity); // Instantiate the cutted tree prefab at the current position
        Vector3 newPosition = cuttedObject.transform.position;
        newPosition.y -= 0.7270002f;
        cuttedObject.transform.position = newPosition;
        Destroy(gameObject); // Destroy the tree game object
    }
}
