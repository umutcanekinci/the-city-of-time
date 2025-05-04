using TMPro;
using UnityEngine;
public class NameText : MonoBehaviour
{
    [SerializeField] private string objectName;
    [SerializeField] private Color nameColor = Color.white; // Color of the name text
    [SerializeField] private GameObject nameCanvasPrefab; // Prefab for the name canvas
    private GameObject nameText; // Prefab for the name text
    private GameObject input; // Prefab for the input text
    public string Name { get { return objectName; } } // Property to get the object's name
    
    void Start()
    {
        if(objectName == null || objectName == "") // Check if the object name is not set
            objectName = gameObject.name; // Set the object name to the GameObject's name
            
        UpdateName(); // Call the UpdateName method to set the name text
    }

    public void UpdateName() {
        GameObject nameCanvas = Instantiate(nameCanvasPrefab, transform); // Instantiate the name canvas prefab
        nameText = nameCanvas.transform.GetChild(0).gameObject;
        input = nameCanvas.transform.GetChild(1).gameObject; // Get the input text from the name canvas
        nameText.GetComponent<TMP_Text>().text = objectName; // Set the name text to the object's name
        nameText.GetComponent<TMP_Text>().color = nameColor; // Set the color of the name text
    }

    public void Highlight(bool highlight)
    {
        nameText.SetActive(!highlight);
        input.SetActive(highlight);
    }
}
