using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public float runSpeed = 2.0f; // Speed at which the enemy runs away
    public GameObject enemyPrefab; // Prefab of the enemy to be cloned
    public float spawnInterval = 5f; // Interval in seconds for cloning the enemy
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

        // Start the coroutine to spawn enemies at intervals
        StartCoroutine(SpawnEnemies());

        // Start the coroutine to rotate the enemy periodically
        StartCoroutine(RotatePeriodically());

        // Play the running animation
        animator.SetBool("isRunning", true);
        Debug.Log("Running animation started for " + gameObject.name);
    }

    void Update()
    {
        // Move the enemy forward in a straight line
        rb.linearVelocity = transform.forward * runSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the enemy hits a wall, change its color to a random color
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Hit a wall, changing color...");
            ChangeColorToRandom();
        }
    }

    void ChangeColorToRandom()
    {
        // Generate a random color
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        renderer.material.color = randomColor; // Apply the random color
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
