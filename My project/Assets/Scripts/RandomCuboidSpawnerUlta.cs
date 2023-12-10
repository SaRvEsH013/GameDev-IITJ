using UnityEngine;

public class RandomCuboidSpawnerUlta : MonoBehaviour
{
    public GameObject[] cuboidPrefabs; // Array of different cuboid prefabs
    public Material[] cuboidMaterials; // Array of different materials for cuboids
    public Vector3 spawnPosition = new Vector3(30.4f, 0.0104157329f, 3.65173063f); // Fixed spawn position
    public Vector3 moveDirection = Vector3.left; // Direction in which the cuboid will move (negative x direction)
    public float moveSpeed = 200f; // Speed of the cuboid
    public float travelDistance = 80f; // Distance the cuboid will travel before being destroyed
    public float minSpawnInterval = 1f; // Minimum time interval between two spawns
    public float maxSpawnInterval = 2f; // Maximum time interval between two spawns

    void Start()
    {
        SpawnRandomCuboid();
    }

    void SpawnRandomCuboid()
    {
        // Select a random cuboid from the array
        GameObject selectedCuboidPrefab = cuboidPrefabs[Random.Range(0, cuboidPrefabs.Length)];

        // Spawn the selected cuboid at the fixed position
        GameObject spawnedCuboid = Instantiate(selectedCuboidPrefab, spawnPosition, Quaternion.identity);

        // Set the spawned cuboid's initial direction and speed
        if (spawnedCuboid.TryGetComponent<Rigidbody>(out var cuboidRigidbody))
        {
            cuboidRigidbody.velocity = moveDirection.normalized * moveSpeed;
        }

        // Apply a random material to the spawned cuboid
        Renderer cuboidRenderer = spawnedCuboid.GetComponent<Renderer>();
        if (cuboidRenderer != null && cuboidMaterials.Length > 0)
        {
            Material selectedMaterial = cuboidMaterials[Random.Range(0, cuboidMaterials.Length)];
            cuboidRenderer.material = selectedMaterial;
        }

        // Destroy the cuboid after it travels a fixed distance
        Destroy(spawnedCuboid, travelDistance / Mathf.Abs(moveSpeed));

        // Schedule the next spawn
        Invoke(nameof(SpawnRandomCuboid), Random.Range(minSpawnInterval, maxSpawnInterval));
    }
}
