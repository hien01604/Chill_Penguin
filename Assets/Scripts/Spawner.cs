using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Pipes prefab; // Pipe prefab
    public float minHeight = 2f; // Minimum spawn height
    public float maxHeight = 6f; // Maximum spawn height
    public float spawnInterval = 3f; // Interval between pipe spawns
    public float minGapSize = 2f; // Minimum gap size between pipes
    public float maxGapSize = 4f; // Maximum gap size between pipes

    private bool isSpawning = false; // Spawning state
    private string currentMode; // Current game mode

    public void EnableSpawning(string mode)
    {
        currentMode = mode; // Set the game mode
        isSpawning = true; // Enable spawning
        InvokeRepeating(nameof(SpawnPipe), 0f, spawnInterval); // Start spawning pipes
    }

    public void DisableSpawning()
    {
        isSpawning = false; // Disable spawning
        CancelInvoke(nameof(SpawnPipe)); // Stop spawning
    }

    public void ResetSpawner()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject); // Destroy all spawned pipes
        }
        CancelInvoke(nameof(SpawnPipe)); // Cancel any remaining invoke calls
    }

    private void SpawnPipe()
    {
        if (!isSpawning) return;

        // Create a new pipe instance
        Pipes newPipe = Instantiate(prefab, transform.position, Quaternion.identity);

        // Randomize the pipe height
        float randomHeight = Random.Range(minHeight, maxHeight);
        newPipe.transform.position += Vector3.up * randomHeight;

        // Randomize the gap size
        float randomGapSize = Random.Range(minGapSize, maxGapSize);
        newPipe.SetGapSize(randomGapSize);

        // Adjust pipe behavior based on the selected mode
        if (currentMode == "Normal")
        {
            newPipe.SetHorizontalOnly(); // Only horizontal movement
        }
        else if (currentMode == "Advanced")
        {
            newPipe.SetHorizontalAndVertical(); // Horizontal and vertical movement
        }

        newPipe.transform.parent = transform; // Set the spawner as the parent
    }
}
