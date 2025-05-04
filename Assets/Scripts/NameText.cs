using TMPro;
using UnityEngine;
public class NameText : MonoBehaviour
{
    
    [SerializeField] private GameObject nameCanvasPrefab; // Prefab for the name canvas
    private GameObject nameText; // Prefab for the name text
    private GameObject input; // Prefab for the input text
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject nameCanvas = Instantiate(nameCanvasPrefab, transform); // Instantiate the name canvas prefab
        nameText = nameCanvas.transform.GetChild(0).gameObject;
        input = nameCanvas.transform.GetChild(1).gameObject; // Get the input text from the name canvas
        nameText.GetComponent<TMP_Text>().text = transform.name;
    }

    public void Highlight(bool highlight)
    {
        nameText.SetActive(!highlight);
        input.SetActive(highlight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
