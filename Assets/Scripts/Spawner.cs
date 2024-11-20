using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Pipes prefab;
    public float minHeight = 5f;
    public float maxHeight = 8f;
    public float verticalGap = 1f;

    public float horizontalGap = 6f; // Khoảng cách giữa các ống theo chiều ngang

    private GameManager gameManager;
    private float lastSpawnXPosition = 0f;

    private void Start()
    {
        // Lấy tham chiếu đến GameManager
        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("GameManager is not assigned!");
            return;
        }

        // Bắt đầu spawn pipes theo tốc độ spawnRate
        InvokeRepeating(nameof(Spawn), gameManager.spawnRate, gameManager.spawnRate);
    }

    private void OnEnable()
    {
        if (gameManager == null)
        {
            gameManager = GameManager.Instance;
            if (gameManager == null)
            {
                Debug.LogError("GameManager is not assigned!");
                return;
            }
        }

        // Bắt đầu spawn pipes khi vào chế độ chơi
        InvokeRepeating(nameof(Spawn), gameManager.spawnRate, gameManager.spawnRate);
    }

    private void OnDisable()
    {
        // Dừng spawn khi không cần thiết
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        // Tạo một pipe mới
        Pipes pipes = Instantiate(prefab, transform.position, Quaternion.identity);

        // Tính toán vị trí mới theo chiều ngang và dọc
        lastSpawnXPosition += horizontalGap;  // Tăng khoảng cách giữa các pipe
        pipes.transform.position = new Vector3(lastSpawnXPosition, transform.position.y, 0f);

        // Điều chỉnh khoảng cách dọc (gap) giữa các phần trên và dưới của pipe
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
        pipes.gap = verticalGap;

        // Set the speed of the pipes based on the GameManager settings
        pipes.speed = gameManager.pipeSpeed;
    }
}
