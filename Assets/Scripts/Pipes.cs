using UnityEngine;

public class Pipes : MonoBehaviour
{
    public Transform top;
    public Transform bottom;
    public float gap = 3f;
    public float speed; // Tốc độ di chuyển của các ống
    public float moveSpeed = 1f;  // Tốc độ di chuyển lên/xuống của các ống
    public float moveRange = 3f;  // Khoảng cách tối đa mà các ống có thể di chuyển lên xuống

    private float leftEdge;
    private GameManager gameManager;  // Tham chiếu đến GameManager
    private float initialY;  // Vị trí ban đầu của cột

    private void Start()
    {
        // Lấy tham chiếu GameManager
        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("GameManager is not assigned!");
            return;
        }

        // Lưu vị trí Y ban đầu của cột
        initialY = transform.position.y;

        // Đặt tốc độ di chuyển của ống từ GameManager
        speed = gameManager.pipeSpeed;

        // Tính toán vị trí ban đầu của các phần trên và dưới của ống
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
        top.position += Vector3.up * gap / 2;
        bottom.position += Vector3.down * gap / 2;
    }

    private void Update()
    {
        // Cập nhật tốc độ của ống từ GameManager
        if (gameManager != null)
        {
            speed = gameManager.pipeSpeed;
        }

        // Di chuyển các ống sang trái
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Di chuyển các ống lên và xuống theo thời gian
        float newY = initialY + Mathf.Sin(Time.time * moveSpeed) * moveRange;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Kiểm tra và hủy ống khi chúng ra khỏi màn hình
        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }
}
