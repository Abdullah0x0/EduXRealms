using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public float runSpeed = 2.0f; // Speed at which the enemy runs away
    public GameObject enemyPrefab; // Prefab of the enemy to be cloned
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
        SpawnEnemyAtStart();

        // Start the coroutine to rotate the enemy periodically
        StartCoroutine(RotatePeriodically());

        // Play the running animation
        animator.SetBool("isRunning", true);
        Debug.Log("Running animation started for " + gameObject.name);
    }

    void Update()
    {
        // Move the enemy forward in a straight line
        rb.velocity = transform.forward * runSpeed;
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

    void SpawnEnemyAtStart()
    {
        // Find the player object (ensure the player has the tag "Player")
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found in the scene. Make sure the player has the tag 'Player'.");
            return;
        }

        // Calculate a spawn position a few units in front of the player
        Vector3 playerPosition = player.transform.position;
        Vector3 forwardDirection = player.transform.forward;
        Vector3 spawnPosition = playerPosition + forwardDirection * 5f; // Adjust the distance as needed

        // Perform a raycast to ensure the position is within the scan mesh
        RaycastHit hit;
        if (Physics.Raycast(playerPosition, forwardDirection, out hit, 10f))
        {
            // If the raycast hits the scan mesh, adjust the spawn position
            spawnPosition = hit.point;
        }
        else
        {
            // If no hit, you might want to handle this case differently
            Debug.LogWarning("Spawn position is not within the scan mesh.");
        }

        // Instantiate the enemy at the calculated position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Spawned a new enemy at position: " + spawnPosition);
    }
}
