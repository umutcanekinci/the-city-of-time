using System.Collections;
using TMPro;
using UnityEngine;

public class FadeOutText : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform; // Reference to the RectTransform component
    [SerializeField] private TextMeshProUGUI textMeshPro; // Reference to the TextMeshProUGUI component
    [SerializeField] private float fadeDuration = 2f; // Duration of the fade-in effect
    [SerializeField] private float delayBeforeFade = 0.5f; // Delay before starting the fade-in effect
    [SerializeField] private float targetAlpha = 0f; // Target alpha value for the fade-in effect
    [SerializeField] private float startAlpha = 1f; // Starting alpha value for the fade-in effect
    [SerializeField] private float flySpeed = 1f; // Speed of the text flying in
    [SerializeField] private Vector2 flyDirection = new Vector2(0, 1); // Direction of the text flying in
    private Vector2 startPosition; // Starting position of the text
    public void Start()
    {
        startPosition = rectTransform.anchoredPosition; // Get the initial position of the text
        textMeshPro.alpha = startAlpha; // Set the initial alpha value
        StartCoroutine(FadeInTextCoroutine()); // Start the fade-in coroutine
    }

    private IEnumerator FadeInTextCoroutine()
    {
        yield return new WaitForSeconds(delayBeforeFade); // Wait for the specified delay

        float elapsedTime = 0f; // Initialize elapsed time
        Color textColor = textMeshPro.color; // Get the current color of the text

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime; // Increment elapsed time
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration); // Calculate the new alpha value
            textMeshPro.color = new Color(textColor.r, textColor.g, textColor.b, alpha); // Set the new color with the updated alpha
            textMeshPro.rectTransform.anchoredPosition += flyDirection * flySpeed * Time.deltaTime; // Move the text in the specified direction
            yield return null; // Wait for the next frame
        }

        textMeshPro.color = new Color(textColor.r, textColor.g, textColor.b, targetAlpha); // Ensure the final alpha value is set
    }

    public void SetText(string text)
    {
        textMeshPro.text = text; // Set the text of the TextMeshProUGUI component
    }

    public void SetColor(Color color)
    {
        textMeshPro.color = color; // Set the color of the TextMeshProUGUI component
    }

    public void Reset()
    {
        textMeshPro.alpha = startAlpha; // Reset the alpha value to the starting value
        rectTransform.anchoredPosition = startPosition; // Reset the position to the starting position
        textMeshPro.text = ""; // Clear the text
    }
}
