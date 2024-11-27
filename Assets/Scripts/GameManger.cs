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

    private HighscoreHandler highscoreHandler; // Declare HighscoreHandler reference

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
        DebugCheckReferences(); // Ensure all references are assigned

        // Retrieve selected mode from PlayerPrefs
        selectedMode = PlayerPrefs.GetString("GameMode", "Normal");
        Debug.Log($"Selected Mode: {selectedMode}");

        // Pause the game initially
        Pause();

        // Dynamically find HighscoreHandler if not assigned
        highscoreHandler = FindObjectOfType<HighscoreHandler>();
        if (highscoreHandler == null)
        {
            Debug.LogError("HighscoreHandler is not assigned in the scene!");
        }
        else
        {
            Debug.Log("HighscoreHandler successfully found in the scene.");
        }
    }   

    public void Pause()
    {
        Time.timeScale = 0f;
        if (player != null) player.enabled = false; // Disable player movement
        if (spawner != null) spawner.DisableSpawning(); // Stop pipe spawning
    }

    public void Play()
    {
        ResetGame(); // Add this to ensure the game resets properly

        playButton.SetActive(false);
        menuButton.SetActive(false);
        highScoreButton.SetActive(false);

        if (spawner != null)
        {
            spawner.ResetSpawner(); // Reset spawner logic
            spawner.EnableSpawning(selectedMode); // Enable spawning again
        }

        if (player != null)
        {
            player.ResetPlayer(); // Ensure the player is reset to initial state
            player.enabled = true; // Re-enable player movement
        }

        Time.timeScale = 1f;
        Debug.Log("Game started!");
    }

    private void ResetGame()
    {
        score = 0;
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }

        if (player != null)
        {
            player.ResetPlayer(); // Call ResetPlayer to reset the player state
        }

        if (spawner != null)
        {
            spawner.ResetSpawner(); // Reset spawner state
        }

        Time.timeScale = 10f;
        Debug.Log("Game reset!");
    }



    public void GameOver()
    {
        HighscoreHandler highscoreHandler = FindObjectOfType<HighscoreHandler>();
        if (highscoreHandler == null)
        {
            Debug.LogError("HighscoreHandler is not assigned in the scene!");
            return;
        }

        string playerName = PlayerPrefs.GetString("PlayerName", "Unknown");
        int currentScore = score;

        highscoreHandler.AddHighscoreIfPossible(new HighscoreElement(playerName, currentScore));

        Pause();

        // Show game over UI
        playButton.SetActive(true);
        menuButton.SetActive(true);
        highScoreButton.SetActive(true);

    }



    public void IncreaseScore()
    {
        score++;
        if (scoreText != null)
            scoreText.text = score.ToString();
    }

    public void Mode()
    {
        Time.timeScale = 1f; // Resume time scale
        SceneManager.LoadScene(2); // Load the main menu scene
    }
    public void Quit()
    {
        Time.timeScale = 1f; // Resume time scale
        SceneManager.LoadScene(0); // Load the main menu scene
    }

    public void ViewHighScore()
    {
        Time.timeScale = 1f;
        Debug.Log("High Score Button Clicked");
        SceneManager.LoadScene(4); // Navigate to high-score scene
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
