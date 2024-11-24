using UnityEngine;

public class Player : MonoBehaviour
{
    public Sprite[] sprites;
    public float baseStrength = 5f;  // Lực nhảy cơ bản
    public float baseGravity = -9.81f;  // Lực hấp dẫn cơ bản
    public float tilt = 5f;  // Lượng nghiêng

    private SpriteRenderer spriteRenderer;
    private Vector3 direction;
    private int spriteIndex;
    private GameManager gameManager;

    private float currentStrength;
    private float currentGravity;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;  // Tham chiếu đến GameManager
        UpdatePlayerStats();  // Cập nhật lực nhảy và hấp dẫn ban đầu
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
    {
        // Nếu cấp độ thay đổi, cập nhật lại strength và gravity
        if (gameManager.level > 0)
        {
            UpdatePlayerStats();  // Cập nhật lực nhảy và hấp dẫn theo cấp độ
        }

        // Kiểm tra nhấn phím để nhảy
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * currentStrength;  // Thực hiện nhảy với lực nhảy hiện tại
        }

        // Áp dụng lực hấp dẫn và cập nhật vị trí của player
        direction.y += currentGravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        // Điều chỉnh độ nghiêng của player dựa trên hướng di chuyển
        Vector3 rotation = transform.eulerAngles;
        rotation.z = direction.y * tilt;
        transform.eulerAngles = rotation;
    }

    private void UpdatePlayerStats()
    {
        // Cập nhật strength và gravity theo cấp độ hiện tại
        currentStrength = baseStrength + gameManager.level * 0.5f;  // Tăng lực nhảy theo cấp độ
        currentGravity = baseGravity - gameManager.level * 0.5f;  // Tăng lực hấp dẫn theo cấp độ
    }

    private void AnimateSprite()
    {
        // Animation sprite của player
        spriteIndex++;
        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }

        if (spriteIndex < sprites.Length && spriteIndex >= 0)
        {
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Xử lý va chạm với chướng ngại vật (game over)
        if (other.gameObject.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
        // Xử lý khi player chạm vào phần scoring (tăng điểm)
        else if (other.gameObject.CompareTag("Scoring"))
        {
            GameManager.Instance.IncreaseScore();
        }
    }
}
