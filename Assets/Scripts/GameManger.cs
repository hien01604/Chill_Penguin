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
        Time.timeScale = 0f;  // Dừng game
        player.enabled = false;  // Tạm dừng player
    }

    public void Play()
    {
        score = 0;
        level = 1;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);

        // Đảm bảo spawn pipes lại từ đầu
        spawner.ResetSpawner();  // Gọi lại hàm reset spawn

        Time.timeScale = 1f;  // Tiếp tục game
        player.enabled = true;  // Bật player lại
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

        // Tăng tốc độ của ống và giảm spawn rate khi lên cấp
        pipeSpeed = Mathf.Min(6f, pipeSpeed + 0.5f); // Đảm bảo tốc độ ống không quá nhanh
        spawnRate = Mathf.Max(0.5f, spawnRate - 0.1f); // Giảm spawn rate nhưng không thấp hơn 0.5 giây

        Debug.Log("Level Up! New Level: " + level);
        Debug.Log("New Pipe Speed: " + pipeSpeed);
    }
}
