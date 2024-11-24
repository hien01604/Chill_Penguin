using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float baseAnimationSpeed = 1f; // Tốc độ animation cơ bản
    private float animationSpeed;  // Tốc độ animation thực tế
    private MeshRenderer meshRenderer;
    private GameManager gameManager;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        gameManager = GameManager.Instance;  // Tham chiếu đến GameManager
    }

    private void Start()
    {
        // Cập nhật tốc độ animation ngay từ đầu
        UpdateAnimationSpeed();
    }

    private void Update()
    {
        // Kiểm tra xem cấp độ có thay đổi không, nếu có, cập nhật tốc độ
        if (gameManager.level > 0)
        {
            UpdateAnimationSpeed();
        }

        // Di chuyển nền theo tốc độ animationSpeed
        meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
    }

    private void UpdateAnimationSpeed()
    {
        // Tính toán tốc độ mới của nền mỗi khi cấp độ thay đổi
        animationSpeed = baseAnimationSpeed + gameManager.level * 0.2f;  // Tăng tốc độ theo cấp độ
    }
}
