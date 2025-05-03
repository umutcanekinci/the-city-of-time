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
        dialogue.StartDialogue(npcName, conversationLines); // Start the dialogue with the conversation lines
    }

}
