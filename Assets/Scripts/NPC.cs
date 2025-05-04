using System;
using UnityEngine;
using TMPro;
using System.Collections;
public class NPC : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue; // Reference to the dialogue object
    [SerializeField] private string[] conversationLines; // Array of conversation lines for the NPC
    
    public void Talk()
    {
        dialogue.StartDialogue(transform.name, conversationLines); // Start the dialogue with the conversation lines
    }

}
