using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreHandler : MonoBehaviour
{
    private List<HighscoreElement> highscoreList = new List<HighscoreElement>();
    [SerializeField] private int maxCount = 5; // Max number of high scores
    [SerializeField] private string filename = "Highscores.json"; // Default filename

    // Delegate and event for when the high-score list changes
    public delegate void OnHighscoreListChanged(List<HighscoreElement> list);
    public static event OnHighscoreListChanged onHighscoreListChanged;

    private void Start()
    {
        // Log the file path to easily locate it
        Debug.Log($"Highscores.json file path: {Application.persistentDataPath}/Highscores.json");

        // Load highscores
        LoadHighscores();
    }


    /// <summary>
    /// Load the high scores from a JSON file.
    /// </summary>
    private void LoadHighscores()
    {
        highscoreList = FileHandler.ReadListFromJSON<HighscoreElement>(filename);
        if (highscoreList == null)
        {
            highscoreList = new List<HighscoreElement>();
        }

        // Ensure the list does not exceed the maximum count
        while (highscoreList.Count > maxCount)
        {
            highscoreList.RemoveAt(highscoreList.Count - 1);
        }

        // Notify listeners about the high-score list update
        if (onHighscoreListChanged != null)
        {
            highscoreList.Sort((a, b) => b.points.CompareTo(a.points)); // Descending order
    
            onHighscoreListChanged.Invoke(highscoreList);
        }

        Debug.Log("High scores loaded.");
        highscoreList = FileHandler.ReadListFromJSON<HighscoreElement>(filename);
        Debug.Log($"Highscores Loaded: {highscoreList.Count} entries.");

    }

    /// <summary>
    /// Save the high scores to a JSON file.
    /// </summary>
    private void SaveHighscore()
    {
        FileHandler.SaveToJSON(highscoreList, filename);
        Debug.Log("High scores saved.");
    }

    /// <summary>
    /// Add a high score if it qualifies for the leaderboard.
    /// </summary>
    /// <param name="element">The new high-score entry.</param>
    public void AddHighscoreIfPossible(HighscoreElement element)
    {
        for (int i = 0; i < maxCount; i++)
        {
            // Check if the current score qualifies for the leaderboard
            if (i >= highscoreList.Count || element.points > highscoreList[i].points)
            {
                highscoreList.Insert(i, element);
                Debug.Log($"Added new high score: {element.playerName} with {element.points} points");

                // Ensure the list does not exceed the maximum count
                while (highscoreList.Count > maxCount)
                {
                    highscoreList.RemoveAt(highscoreList.Count - 1);
                }

                SaveHighscore();

                // Notify listeners about the high-score list update
                if (onHighscoreListChanged != null)
                {
                    onHighscoreListChanged.Invoke(highscoreList);
                }

                return;
            }
        }

        Debug.Log("Score did not qualify for the leaderboard.");
    }
}
