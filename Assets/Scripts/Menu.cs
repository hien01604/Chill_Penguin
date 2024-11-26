using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // This method is linked to the "Start" button in the UI
    public void OnStartButtonPressed()
    {
        // Load the scene with build index 1 (set this in the Build Settings)
        SceneManager.LoadScene(1);
    }

    // This method is linked to the "Quit" button in the UI
    public void OnQuitButtonPressed()
    {
        // Quit the application
        Application.Quit();
        // Log for testing in the Unity Editor (since Quit won't work in the Editor)
        Debug.Log("Game is quitting...");
    }
}
