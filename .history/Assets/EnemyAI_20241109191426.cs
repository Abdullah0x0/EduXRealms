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

        // Start the coroutine to spawn enemies at intervals
        StartCoroutine(SpawnEnemies());

        // Play the running animation
        animator.SetBool("isRunning", true);
    }

    void Update()
    {
        // Move the enemy forward in a straight line
        rb.linearVelocity = transform.forward * runSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the enemy hits a wall, choose a new random direction
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Generate a random angle to turn away from the wall
            // float randomAngle = Random.Range(90f, 270f);
            // transform.Rotate(0, randomAngle, 0);
            transform.Rotate(0, 180.0f, 0);
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
        }
    }
}
