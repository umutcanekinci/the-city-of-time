using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private NPC npc; // Reference to the NPC object
    public void Interact()
    {
        npc.Talk(); // Call the Talk method on the NPC to start the dialogue    
    }

    public NPC GetNPC()
    {
        return npc; // Return the NPC object
    }
}
