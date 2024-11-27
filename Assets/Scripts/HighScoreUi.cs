using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace


public class HighscoreUI : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject highscoreUIElementPrefab;
    [SerializeField] Transform elementWrapper;

    List<GameObject> uiElements = new List<GameObject>();

    private void OnEnable()
    {
        HighscoreHandler.onHighscoreListChanged += UpdateUI;
    }

    private void OnDisable()
    {
        HighscoreHandler.onHighscoreListChanged -= UpdateUI;
    }

    public void ShowPanel()
    {
        panel.SetActive(true);
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
        SceneManager.LoadScene(3);
    }

    private void UpdateUI(List<HighscoreElement> list)
    {
        Debug.Log("Updating Highscore UI...");

        // Ensure the UI elements match the list size
        while (uiElements.Count < list.Count)
        {
            var inst = Instantiate(highscoreUIElementPrefab, Vector3.zero, Quaternion.identity);
            inst.transform.SetParent(elementWrapper, false);
            uiElements.Add(inst);
            Debug.Log("Instantiated new highscore UI element.");
        }

        for (int i = 0; i < uiElements.Count; i++)
        {
            if (i < list.Count)
            {
                HighscoreElement el = list[i];
                Debug.Log($"UI Update - Name: {el.playerName}, Points: {el.points}");

                // Fetch all Text components in the UI element
                var texts = uiElements[i].GetComponentsInChildren<TMP_Text>();
                Debug.Log($"Found {texts.Length} Text components in UI element at index {i}.");

                if (texts.Length >= 2)
                {
                    // Example: Formatting text for consistent alignment
                    texts[0].text = el.playerName; // Adjust padding as necessary
                    texts[1].text = el.points.ToString();
                    var nameText = uiElements[i].transform.Find("Name").GetComponent<TMP_Text>();
                    var scoreText = uiElements[i].transform.Find("Score").GetComponent<TMP_Text>();

                    if (nameText != null)
                    {
                        nameText.text = el.playerName; // Set the player's name
                    }
                    else
                    {
                        Debug.LogWarning("Name Text component not found in HighscoreElement.");
                    }

                    if (scoreText != null)
                    {
                        scoreText.text = el.points.ToString(); // Set the player's score
                    }
                    else
                    {
                        Debug.LogWarning("Score Text component not found in HighscoreElement.");
                    }

                }
                else
                {
                    Debug.LogWarning($"UI Element at index {i} does not have enough Text components.");
                }

                uiElements[i].SetActive(true); // Ensure the element is visible
            }
            else
            {
                Debug.Log($"Hiding extra UI element at index {i}.");
                uiElements[i].SetActive(false); // Hide extra UI elements
            }
        }
    }
}
