using UnityEngine;
using System.Collections; // Include this namespace for IEnumerator

public class EnemyAI : MonoBehaviour
{
    public float runSpeed = 2.0f; // Speed at which the enemy runs away
    public GameObject enemyPrefab; // Prefab of the enemy to be spawned
    private Animator animator; // Reference to the Animator component
    private Rigidbody rb; // Reference to the Rigidbody component
    private Renderer renderer; // Reference to the Renderer component

    void Start()
    {
        // Get the Animator, Rigidbody, and Renderer components attached to the enemy
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();

        if (animator == null)
        {
            Debug.LogError("Animator component is missing on " + gameObject.name);
        }
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing on " + gameObject.name);
        }
        if (renderer == null)
        {
            Debug.LogError("Renderer component is missing on " + gameObject.name);
        }

        // Spawn the enemy once at the start
        // SpawnEnemy();

        // Start the coroutine to rotate the enemy periodically
        StartCoroutine(RotatePeriodically());

        // Play the running animation
        animator.SetBool("isRunning", true);
        Debug.Log("Running animation started for " + gameObject.name);
    }

    void Update()
    {
        // Move the enemy forward in a straight line
        transform.up = Vector3(0.0f, 1.0f, 0.0f);
        rb.linearVelocity = transform.forward * runSpeed;
    }

    void SpawnEnemy()
    {
        // Spawn the enemy at the origin, one foot above the ground (assuming 1 unit = 1 meter)
        Vector3 spawnPosition = new Vector3(0, 0.3f, 0); // 0.3f meters is approximately 1 foot
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Spawned the enemy at position: " + spawnPosition);
    }

    IEnumerator RotatePeriodically()
    {
        while (true)
        {
            // Wait for 3 seconds
            yield return new WaitForSeconds(3f);

            // Rotate the enemy by a random angle between 90 and 270 degrees
            float randomAngle = Random.Range(90f, 270f);
            transform.Rotate(0, randomAngle, 0);
            Debug.Log("Enemy rotated by " + randomAngle + " degrees");
        }
    }
}
