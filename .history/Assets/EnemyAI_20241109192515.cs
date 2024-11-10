using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public float runSpeed = 2.0f; // Speed at which the enemy runs away
    public GameObject enemyPrefab; // Prefab of the enemy to be cloned
    public float spawnInterval = 5f; // Interval in seconds for cloning the enemy
    private Animator animator; // Reference to the Animator component
    private Rigidbody rb; // Reference to the Rigidbody component

    void Start()
    {
        // Get the Animator and Rigidbody components attached to the enemy
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        if (animator == null)
        {
            Debug.LogError("Animator component is missing on " + gameObject.name);
        }
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing on " + gameObject.name);
        }

        // Start the coroutine to spawn enemies at intervals
        StartCoroutine(SpawnEnemies());

        // Play the running animation
        animator.SetBool("isRunning", true);
        Debug.Log("Running animation started for " + gameObject.name);
    }

    void Update()
    {
        // Move the enemy forward in a straight line
        rb.velocity = transform.forward * runSpeed;
        Debug.Log("Moving forward with velocity: " + rb.velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        // If the enemy hits a wall, choose a new random direction
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Hit a wall, rotating the enemy...");
            transform.Rotate(0, 180.0f, 0); // Rotate 180 degrees
            Debug.Log("Enemy rotated to: " + transform.rotation.eulerAngles);
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Wait for the specified interval
            yield return new WaitForSeconds(spawnInterval);

            // Clone the enemy
            Instantiate(enemyPrefab, transform.position, transform.rotation);
            Debug.Log("Spawned a new enemy at position: " + transform.position);
        }
    }
}
