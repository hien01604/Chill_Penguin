using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Required for TMP_InputField

public class InputName : MonoBehaviour
{
    public TMP_InputField nameInputField; // Change InputField to TMP_InputField

    private void Start()
    {
        // Automatically find the TMP_InputField if not assigned
        if (nameInputField == null)
        {
            nameInputField = FindObjectOfType<TMP_InputField>();
            if (nameInputField == null)
            {
                Debug.LogError("No TMP_InputField found in the scene! Please assign it in the Inspector.");
            }
        }
    }

    public void OnNameValueChanged(string input)
    {
        Debug.Log("Current input: " + input);
    }

    public void OnNameEndEdit(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            Debug.LogWarning("Name cannot be empty!");
        }
        else
        {
            PlayerPrefs.SetString("PlayerName", input);
            Debug.Log("Player name saved: " + input);
            PlayerPrefs.Save();

        }
    }

    public void OnBackButtonPressed()
    {
        SceneManager.LoadScene(0); // Load the Main Menu scene
    }

    public void OnNextButtonPressed()
    {
        if (nameInputField == null)
        {
            Debug.LogError("nameInputField is not assigned or found!");
            return;
        }

        if (string.IsNullOrEmpty(nameInputField.text))
        {
            Debug.Log("Please enter a name!");
        }
        else
        {
            PlayerPrefs.SetString("PlayerName", nameInputField.text);
            PlayerPrefs.Save();
            Debug.Log("Player name saved: " + nameInputField.text);
            SceneManager.LoadScene(2); // Load the next scene
        }
    }
}
