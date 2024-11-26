using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelection : MonoBehaviour
{
    // Method to select Normal mode
    public void SelectNormalMode()
    {
        SetGameModeAndLoadScene("Normal");
    }

    // Method to select Advanced mode
    public void SelectAdvancedMode()
    {
        SetGameModeAndLoadScene("Advanced");
    }

    // Common method to set the mode and load the game scene
    private void SetGameModeAndLoadScene(string mode)
    {
        // Save the selected mode in PlayerPrefs
        PlayerPrefs.SetString("GameMode", mode);
        Debug.Log($"GameMode set to: {mode}");

        // Load the unified game scene (replace "3" with the actual scene name or index)
        SceneManager.LoadScene(3);
    }
}
