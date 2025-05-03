using UnityEngine;
using TMPro;
using System.Collections;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Reference to the TextMeshProUGUI component for displaying dialogue
    public string[] dialogueLines; // Array of dialogue lines to display
    public float typingSpeed = 0.05f; // Speed at which the text is typed out

    private int currentLineIndex = 0; // Index of the current dialogue line being displayed

    void Start()
    {
        dialogueText.text = ""; // Clear the text at the start
    }

    public void StartDialogue()
    {
        Debug.Log("Starting dialogue..."); // Log that the dialogue is starting
        gameObject.SetActive(true); // Activate the dialogue object
        currentLineIndex = 0; // Reset the current line index to 0
        StartCoroutine(TypeLine(dialogueLines[currentLineIndex])); // Start typing the first line of dialogue
    }

    IEnumerator TypeLine(string line)
    {
        dialogueText.text = ""; // Clear the text before typing out the new line
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter; // Add each letter to the text one by one
            yield return new WaitForSeconds(typingSpeed); // Wait for the specified typing speed before adding the next letter
        }
    }
}
