using UnityEngine;
using UnityEngine.UI;
using TMPro;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private Spawner spawner;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameOver;

    public int score { get; private set; } = 0;
    public int level { get; private set; } = 1;
    public float pipeSpeed { get; private set; } = 3f; // Tốc độ của ống
    public float spawnRate { get; private set; } = 1f; // Tốc độ spawn ống

    private int scoreToLevelUp = 10; // Mốc điểm để tăng độ khó

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

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void Play()
    {
        score = 0;
        level = 1;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    public void GameOver()
    {
        playButton.SetActive(true);
        gameOver.SetActive(true);

        Pause();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();

        // Check if score has reached the threshold to level up
        if (score >= scoreToLevelUp * level)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;

        // Increase pipe speed and decrease spawn rate as level goes up
        pipeSpeed += 0.5f; // Tăng tốc độ của ống mỗi khi lên cấp
        spawnRate = Mathf.Max(0.5f, spawnRate - 0.1f); // Giảm spawn rate mỗi khi lên cấp, không thấp hơn 0.5 giây

        Debug.Log("Level Up! New Level: " + level);
        Debug.Log("New Pipe Speed: " + pipeSpeed);
    }
}
