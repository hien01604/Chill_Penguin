using UnityEngine;

public class Pipes : MonoBehaviour
{
    public Transform top; // Top pipe
    public Transform bottom; // Bottom pipe
    public float horizontalSpeed = 2f; // Horizontal movement speed
    public float verticalSpeed = 1f; // Vertical movement speed
    public float verticalRange = 2f; // Range of vertical oscillation
    private bool moveVertically = false; // Whether the pipe should move vertically

    private float initialY; // Initial Y position

    private void Start()
    {
        initialY = transform.position.y; // Save the starting Y position
    }

    private void Update()
    {
        // Horizontal movement
        transform.position += Vector3.left * horizontalSpeed * Time.deltaTime;

        // Vertical movement (if enabled)
        if (moveVertically)
        {
            float newY = initialY + Mathf.Sin(Time.time * verticalSpeed) * verticalRange;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

        // Destroy the pipe when it goes off-screen
        if (transform.position.x < -10f) // Adjust based on your scene setup
        {
            Destroy(gameObject);
        }
    }

    public void SetGapSize(float gapSize)
    {
        // Adjust the gap between top and bottom pipes
        top.position = new Vector3(top.position.x, top.position.y + gapSize / 2, top.position.z);
        bottom.position = new Vector3(bottom.position.x, bottom.position.y - gapSize / 2, bottom.position.z);
    }

    public void SetHorizontalOnly()
    {
        moveVertically = false; // Disable vertical movement
    }

    public void SetHorizontalAndVertical()
    {
        moveVertically = true; // Enable vertical movement
    }
}
