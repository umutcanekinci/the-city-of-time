using System;
using UnityEngine;
using TMPro;
using System.Collections;
public class NPC : MonoBehaviour
{
    public enum NPCState {
        Busy,
        Available,
        
    }

    [SerializeField] private NPCState npcState; // State of the NPC (Available or Busy)

    [SerializeField] private Dialogue dialogue; // Reference to the dialogue object
    [SerializeField] private string[] conversationLines; // Array of conversation lines for the NPC
    [SerializeField] private string busyConversation; // Conversation line when the NPC is busy
    [SerializeField] private Sprite npcSprite; // Sprite of the NPC

    public void Talk()
    {
        string[] conversationLines = new string[1]; // Initialize an empty array for conversation lines
        conversationLines[0] = busyConversation;

        if (npcState == NPCState.Available)
            conversationLines = this.conversationLines;

        dialogue.StartDialogue(gameObject, conversationLines, npcSprite); // Start the dialogue with the conversation lines
    }

    public void SetState(NPCState state)
    {
        npcState = state; // Set the NPC's state to the specified state
    }

}
