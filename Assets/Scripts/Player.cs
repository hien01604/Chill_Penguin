using UnityEngine;

public class Player : MonoBehaviour
{
    public Sprite[] sprites; // Sprites for the bird
    public float jumpForce = 3f;
    public AudioClip jumpSound; // Jump sound
    public AudioClip gameOverSound; // Game-over sound

    private SpriteRenderer spriteRenderer;
    private Vector3 direction;
    private AudioSource audioSource; // AudioSource for playing sounds
    private Rigidbody2D rb;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on the Player object. Add it via the Inspector.");
        }
    }

    private void Update()
    {
        // Handle jump input
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        // Apply gravity
        ApplyGravity();
    }

    private void Jump()
    {
        direction = Vector3.up * jumpForce;

        // Play the jump sound
        PlayJumpSound();

        // Optional: Add animation/sprite switching logic here
    }

    private void ApplyGravity()
    {
        direction.y += Physics2D.gravity.y * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            // Handle collision with obstacle (reduce lives or game over)
            GameManager.Instance.OnCollisionTrigger();

            // Play game-over sound if lives reach zero
            if (GameManager.Instance.lives <= 0)
            {
                PlayGameOverSound();
                enabled = false; // Disable player control
            }
        }
        else if (collision.CompareTag("Scoring"))
        {
            // Increase score when passing scoring trigger
            GameManager.Instance.IncreaseScore();
        }
    }

    public void ResetPlayer()
    {
        // Reset position and velocity
        transform.position = new Vector3(0, 0, 0); // Adjust to starting position if needed
        direction = Vector3.zero;
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }

        // Re-enable controls
        enabled = true;

        // Optionally reset animations or states
        Debug.Log("Player has been reset to initial state.");
    }

    private void PlayJumpSound()
    {
        if (audioSource != null && jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound); // Play jump sound
        }
        else
        {
            Debug.LogWarning("JumpSound or AudioSource is not assigned!");
        }
    }

    private void PlayGameOverSound()
    {
        if (audioSource != null && gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound); // Play game-over sound
        }
        else
        {
            Debug.LogWarning("GameOverSound or AudioSource is not assigned!");
        }
    }
}
