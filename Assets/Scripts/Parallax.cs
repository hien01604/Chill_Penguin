using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float baseAnimationSpeed = 1f; // Base speed of the parallax animation
    private float animationSpeed;  // Effective animation speed
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        // Set the initial animation speed
        animationSpeed = baseAnimationSpeed;
    }

    private void Update()
    {
        // Scroll the background texture horizontally
        meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
    }
}
