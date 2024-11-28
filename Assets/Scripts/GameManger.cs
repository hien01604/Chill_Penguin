using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Audio
    public AudioSource musicAudioSource;
    public AudioClip musicClip;
    public AudioClip gameOverClip;

    // Player, UI, and gameplay components
    [SerializeField] private Player player;
    [SerializeField] private Spawner spawner;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText; // Display lives on screen
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject menuButton;
    [SerializeField] private GameObject highScoreButton;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private GameObject gameOverImage; // Game Over image

    private HighscoreHandler highscoreHandler;

    public int score { get; private set; } = 0;
    public int lives = 1; // Initial lives
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
        // Initialize UI
        if (gameOverImage != null) gameOverImage.SetActive(false);
        UpdateScoreText();
        UpdateLivesText();

        DebugCheckReferences();

        selectedMode = PlayerPrefs.GetString("GameMode", "Normal");
        Debug.Log($"Selected Mode: {selectedMode}");

        Pause();

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
        if (player != null) player.enabled = false;
        if (spawner != null) spawner.DisableSpawning();
    }

    public void Play()
    {
        ResetGame();

        playButton.SetActive(false);
        menuButton.SetActive(false);
        highScoreButton.SetActive(false);
        quitButton.SetActive(false);

        if (spawner != null)
        {
            spawner.ResetSpawner();
            spawner.EnableSpawning(selectedMode);
        }

        if (player != null)
        {
            player.ResetPlayer();
            player.enabled = true;
        }

        Time.timeScale = 1f;
        Debug.Log("Game started!");
    }

    private void ResetGame()
    {
        score = 0;
        lives = 3;
        UpdateScoreText();
        UpdateLivesText();

        if (player != null) player.ResetPlayer();
        if (spawner != null) spawner.ResetSpawner();

        if (gameOverImage != null) gameOverImage.SetActive(false);

        Time.timeScale = 1f;
        Debug.Log("Game reset!");
    }

    public void GameOver()
    {
        if (highscoreHandler != null)
        {
            string playerName = PlayerPrefs.GetString("PlayerName", "Unknown");
            int currentScore = score;
            highscoreHandler.AddHighscoreIfPossible(new HighscoreElement(playerName, currentScore));
        }

        Pause();

        if (gameOverImage != null) gameOverImage.SetActive(true);
        playButton.SetActive(true);
        menuButton.SetActive(true);
        highScoreButton.SetActive(true);
        quitButton.SetActive(true);

        // Play game-over music
        if (musicAudioSource != null && gameOverClip != null)
        {
            musicAudioSource.Stop();
            musicAudioSource.clip = gameOverClip;
            musicAudioSource.loop = false;
            musicAudioSource.Play();
        }
    }

    public void IncreaseScore()
    {
        score++;
        UpdateScoreText();

        if (score % 5 == 0)
        {
            lives++;
            UpdateLivesText();
            Debug.Log("Life gained! Lives: " + lives);
        }
    }

    public void OnCollisionTrigger()
    {
        lives--;
        UpdateLivesText();

        if (lives > 0)
        {
            RespawnPlayer();
        }
        else
        {
            GameOver();
        }
    }

    private void RespawnPlayer()
    {
        if (player != null)
        {
            player.ResetPlayer();
            Debug.Log("Player respawned. Lives left: " + lives);
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    private void UpdateLivesText()
    {
        if (livesText != null)
        {
            livesText.text = lives.ToString();
        }
    }

    public void Mode()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void ViewHighScore()
    {
        Time.timeScale = 1f;
        Debug.Log("High Score Button Clicked");
        SceneManager.LoadScene(4);
    }

    private void DebugCheckReferences()
    {
        if (player == null) Debug.LogError("Player is not assigned in GameManager!");
        if (spawner == null) Debug.LogError("Spawner is not assigned in GameManager!");
        if (scoreText == null) Debug.LogError("ScoreText (TextMeshPro) is not assigned in GameManager!");
        if (livesText == null) Debug.LogError("LivesText (TextMeshPro) is not assigned in GameManager!");
        if (playButton == null) Debug.LogError("PlayButton is not assigned in GameManager!");
        if (menuButton == null) Debug.LogError("MenuButton is not assigned in GameManager!");
        if (highScoreButton == null) Debug.LogError("HighScoreButton is not assigned in GameManager!");
    }
}
