using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float animationSpeed = 1f; // Base animation speed
    private MeshRenderer meshRenderer;
    private GameManager gameManager;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        gameManager = GameManager.Instance;  // Get the GameManager reference
    }

    private void Update()
    {
        // Adjust the animation speed based on the current level
        animationSpeed = 1f + gameManager.level * 0.2f; // Increase speed with level

        // Move the background texture
        meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
    }
}
