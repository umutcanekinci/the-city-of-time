
using UnityEngine;
using UnityEngine.Events;


public class Interaction : MonoBehaviour
{
    // onInteract event
    [SerializeField] private UnityEvent onInteract; // Unity event to be triggered on interaction
    public void Interact()
    {
        onInteract.Invoke();
    }
}
