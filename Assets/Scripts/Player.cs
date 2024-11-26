using UnityEngine;

public class Player : MonoBehaviour
{
    public Sprite[] sprites;
    public float jumpForce = 5f;

    private SpriteRenderer spriteRenderer;
    private Vector3 direction;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Jump on input
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * jumpForce;
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
        }
        else if (collision.CompareTag("Scoring"))
        {
            GameManager.Instance.IncreaseScore();
        }
    }
}
