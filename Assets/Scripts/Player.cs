using UnityEngine;

public class Player : MonoBehaviour
{
    public Sprite[] sprites;
    public float strength = 5f;  // Flapping strength
    public float gravity = -9.81f;  // Gravity
    public float tilt = 5f;  // Tilt amount

    private SpriteRenderer spriteRenderer;
    private Vector3 direction;
    private int spriteIndex;
    private GameManager gameManager;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;  // Reference to GameManager
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
        // Adjust player strength and gravity based on the level
        strength = 5f + gameManager.level * 0.5f;  // Increase strength as level increases
        gravity = -9.81f - gameManager.level * 0.5f;  // Increase gravity as level increases

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;
        }

        // Apply gravity and update the position
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        // Tilt the bird based on the direction
        Vector3 rotation = transform.eulerAngles;
        rotation.z = direction.y * tilt;
        transform.eulerAngles = rotation;
    }

    private void AnimateSprite()
    {
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
        if (other.gameObject.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
        else if (other.gameObject.CompareTag("Scoring"))
        {
            GameManager.Instance.IncreaseScore();
        }
    }
}
