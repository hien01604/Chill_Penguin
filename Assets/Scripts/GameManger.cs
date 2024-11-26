using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private Spawner spawner;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject menuButton;
    [SerializeField] private GameObject highScoreButton;

    public int score { get; private set; } = 0;
    private string selectedMode;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        DebugCheckReferences(); // Check for null references at runtime

        // Retrieve selected mode from PlayerPrefs
        selectedMode = PlayerPrefs.GetString("GameMode", "Normal");
        Debug.Log($"Selected Mode: {selectedMode}");

        // Start the game paused
        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        if (player != null) player.enabled = false; // Disable player controls
        if (spawner != null) spawner.DisableSpawning(); // Stop spawning pipes
    }

    public void Play()
    {
        DebugCheckReferences(); // Verify that all references are set

        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        menuButton.SetActive(false);
        highScoreButton.SetActive(false);

        if (spawner != null) spawner.ResetSpawner();
        if (spawner != null) spawner.EnableSpawning(selectedMode); // Pass the selected mode to EnableSpawning()

        Time.timeScale = 1f;
        if (player != null) player.enabled = true; // Enable player controls
    }

    public void GameOver()
    {
        playButton.SetActive(true);
        menuButton.SetActive(true);
        highScoreButton.SetActive(true);

        Pause();
    }

    public void IncreaseScore()
    {
        score++;
        if (scoreText != null)
            scoreText.text = score.ToString();
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f; // Resume time scale
        SceneManager.LoadScene(2); // Load the main menu scene
    }

    public void ViewHighScore()
    {
        Debug.Log("High Score Button Clicked");
        // Replace with your high-score scene index or name
        SceneManager.LoadScene("HighScoreScene");
    }

    private void DebugCheckReferences()
    {
        if (player == null)
            Debug.LogError("Player is not assigned in GameManager!");

        if (spawner == null)
            Debug.LogError("Spawner is not assigned in GameManager!");

        if (scoreText == null)
            Debug.LogError("ScoreText (TextMeshPro) is not assigned in GameManager!");

        if (playButton == null)
            Debug.LogError("PlayButton is not assigned in GameManager!");

        if (menuButton == null)
            Debug.LogError("MenuButton is not assigned in GameManager!");

        if (highScoreButton == null)
            Debug.LogError("HighScoreButton is not assigned in GameManager!");
    }
}
