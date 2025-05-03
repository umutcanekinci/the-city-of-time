using System;
using UnityEngine;
using TMPro;
using System.Collections;
public class NPC : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue; // Reference to the dialogue object
    [SerializeField] private TextMeshProUGUI nameText; // Reference to the TextMeshProUGUI component for displaying the NPC's name

    private string npcName; // Name of the NPC
    [SerializeField] private string[] conversationLines; // Array of conversation lines for the NPC
    


    public TextMeshProUGUI GetNameText()
    {
        return nameText; // Return the TextMeshProUGUI component for displaying the NPC's name
    }

    public void Start()
    {
        // Initialize the NPC's name and display it in the UI
        npcName = gameObject.name;
        nameText.text = npcName;
    }

    public void Talk()
    {
        dialogue.StartDialogue(conversationLines); // Start the dialogue with the conversation lines
    }

    // private ArrayList getConversation()
    // {
    //     // Return a conversation line based on the NPC's name
    //     switch (npcName)
    //     {
    //         case "Bob":
    //             return new ArrayList() { "Hello, I'm Bob!", "How are you?" };
    //         case "Alice":
    //             return new ArrayList() { "Hi, I'm Alice!", "Nice to meet you!" };
    //         case "Charlie":
    //             return new ArrayList() { "Hey, I'm Charlie!", "What brings you here?" };
    //         default:
    //             return new ArrayList() { "Hi, I'm " + npcName + "!", "Welcome to our world!" };
    //     }   
    // }

}
