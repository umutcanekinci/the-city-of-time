using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using System;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private Image uiImage; // Reference to the Image component for displaying the NPC's sprite
    [SerializeField] private TextMeshProUGUI nameText; // Reference to the TextMeshProUGUI component for displaying the NPC's name
    [SerializeField] private TextMeshProUGUI dialogueText; // Reference to the TextMeshProUGUI component for displaying dialogue
    [SerializeField] private GameObject dialogueBox; // Reference to the dialogue box object
    private ArrayList dialogueLines; // Array of dialogue lines to display
    [SerializeField] private float typingSpeed = 0.05f; // Speed at which the text is typed out
    [SerializeField] private float defaultWaitTime = 1f; // Time to wait before displaying the next line
    [SerializeField] private float waitTimeAfterLastLine = 2f; // Time to wait after the last line before closing the dialogue box
    public event Action<GameObject> onDialogueEnd; // Event to notify when the dialogue ends
    private GameObject talkingObject; // Reference to the current NPC object

    private int currentLineIndex = 0; // Index of the current dialogue line being displayed

    private void Start()
    {
        dialogueText.text = ""; // Clear the text at the start
        dialogueBox.SetActive(false); // Deactivate the dialogue box at the start
    }

    public void AddLine(string line)
    {
        if (dialogueLines == null) // If the dialogue lines array is not initialized, create a new one
            dialogueLines = new ArrayList();

        dialogueLines.Add(line); // Add the new line to the dialogue lines array
    }

    public void StartDialogue(GameObject talkingObject, string[] dialogueLines, Sprite sprite = null)
    {
        if (dialogueBox.activeSelf) return; // If the dialogue object is already active, exit the method
        if (dialogueLines == null || dialogueLines.Length == 0) return; // If there are no dialogue lines, exit the method

        this.talkingObject = talkingObject; // Store the reference to the current NPC object
        nameText.text = talkingObject.GetComponent<NameText>().Name; // Set the NPC's name in the dialogue box

        this.dialogueLines = new ArrayList(); // Initialize the dialogue lines array
        foreach (string line in dialogueLines) // Loop through each line in the provided dialogue lines
            this.dialogueLines.Add(line); // Add each line to the dialogue lines array

        uiImage.sprite = sprite; // Set the NPC's sprite in the dialogue box
        dialogueBox.SetActive(true); // Activate the dialogue object
        currentLineIndex = 0; // Reset the current line index to 0
        StartTyping(); // Start typing the first line of dialogue
    }

    IEnumerator TypeLines(string line, float waitTime)
    {
        dialogueText.text = ""; // Clear the text before typing out the new line
        bool spacePressed = false; // Flag to track if space was pressed

        foreach (char letter in line.ToCharArray())
        {
            if (Input.GetKeyDown(KeyCode.Space) && !spacePressed) // If the space key is pressed and not already processed
            {
                dialogueText.text = line; // Display the entire line immediately
                spacePressed = true; // Mark space as processed
                break; // Exit the loop to stop typing
            }

            dialogueText.text += letter; // Add each letter to the text one by one

            float felapsedTime = 0f;
            float fwaitTime = typingSpeed; // Wait time for typing speed
            while (felapsedTime < fwaitTime)
            {
                if (Input.GetKeyDown(KeyCode.Space) && !spacePressed) // If the space key is pressed and not already processed
                {
                    dialogueText.text = line; // Display the entire line immediately
                    spacePressed = true; // Mark space as processed
                    break; // Exit the wait early
                }
                felapsedTime += Time.deltaTime; // Increment elapsed time
                yield return null; // Wait for the next frame
            }

            if (spacePressed) break; // Exit the loop if space was pressed
        }

        float elapsedTime = 0f;
        while (elapsedTime < waitTime)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !spacePressed) // If the space key is pressed and not already processed
            {
                spacePressed = true; // Mark space as processed
                break; // Exit the wait early
            }
            elapsedTime += Time.deltaTime; // Increment elapsed time
            yield return null; // Wait for the next frame
        }

        DisplayNextDialogueLine();
    }

    private void DisplayNextDialogueLine()
    {
        if (currentLineIndex < dialogueLines.Count - 1) // If there are more lines to display
            StartTyping(); // Start typing the next line
        else // If there are no more lines to display
            CloseDialogueBox(); // Close the dialogue box
    }

    private void StartTyping() {
        float waitTime = dialogueLines.Count - 1 == currentLineIndex ? waitTimeAfterLastLine : defaultWaitTime;
        StartCoroutine(TypeLines(dialogueLines[currentLineIndex].ToString(), waitTime)); // Start typing the next line
        currentLineIndex++; // Move to the next line
    }

    private void CloseDialogueBox()
    {
        dialogueBox.SetActive(false); // Deactivate the dialogue box
        dialogueLines.Clear(); // Clear the dialogue lines array
        currentLineIndex = 0; // Reset the current line index to 0
        onDialogueEnd?.Invoke(talkingObject); // Invoke the dialogue end event
    }
}
