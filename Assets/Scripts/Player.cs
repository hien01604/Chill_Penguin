using UnityEngine;

public class Player : MonoBehaviour
{
    public Sprite[] sprites; // Sprites for the bird
    public float jumpForce = 3f;
    public AudioClip jumpSound; // Add a public variable for the jump sound

    private SpriteRenderer spriteRenderer;
    private Vector3 direction;
    private AudioSource audioSource; // AudioSource component for playing sound

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on the Player object. Add it via the Inspector.");
        }
    }

    private void Update()
    {
        // Jump on input
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * jumpForce;

            // Play the jump sound
            PlayJumpSound();
        }

        // Apply gravity
        direction.y += Physics2D.gravity.y * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
            enabled = false;
        }
        else if (collision.CompareTag("Scoring"))
        {
            GameManager.Instance.IncreaseScore();
        }
    }

    public void ResetPlayer()
    {
        // Reset the player's position to the starting position
        transform.position = new Vector3(0, 0, 0); // Modify this to the desired starting position if needed

        // Reset velocity to stop all movement
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // Re-enable the player movement and controls
        enabled = true;

        // Optionally, reset animations or any player-specific states
        Debug.Log("Player has been reset to initial state.");
    }

    private void PlayJumpSound()
    {
        if (audioSource != null && jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound); // Play the jump sound once
        }
        else
        {
            Debug.LogWarning("AudioSource or JumpSound is not assigned!");
        }
    }
}
