// PlayerController.cs

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    void Update()
    {
        MovePlayer();
        if (Input.GetButtonDown("Fire1")) // Default is left mouse button
        {
            Shoot();
        }
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.position += movement * moveSpeed * Time.deltaTime;
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}


// Bullet.cs

using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;
    public float lifespan = 2f;

    void Start()
    {
        Destroy(gameObject, lifespan); // Destroy the bullet after a set time
    }

    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            Destroy(other.gameObject); // Destroy the zombie
            Destroy(gameObject); // Destroy the bullet
        }
    }
}


// Zombie.cs

using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        // Simple movement towards the player
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            transform.LookAt(player.transform);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}


// ZombieSpawner.cs

using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public float spawnTime = 3f;

    void Start()
    {
        InvokeRepeating("SpawnZombie", 2f, spawnTime);
    }

    void SpawnZombie()
    {
        float x = Random.Range(-10f, 10f);
        float z = Random.Range(-10f, 10f);
        Vector3 spawnPosition = new Vector3(x, 0, z);
        Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
    }
}

