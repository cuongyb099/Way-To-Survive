using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject zombiePrefab;
    public float spawnInterval = 2f;

    void Start()
    {
        InvokeRepeating("SpawnZombie", 0f, spawnInterval);
    }

    void SpawnZombie()
    {
        // Sinh ra zombie tại vị trí ngẫu nhiên
        Vector3 spawnPosition = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
    }
}