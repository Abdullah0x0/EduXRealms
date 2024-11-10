using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public float runSpeed = 2.0f; // Speed at which the enemy runs away
    public GameObject hitEffectPrefab; // Prefab of the particle effect for when the enemy is hit
    public AudioClip deathSound; // Audio clip for the death sound
    private Animator animator; // Reference to the Animator component
    private Rigidbody rb; // Reference to the Rigidbody component
    private Renderer renderer; // Reference to the Renderer component
    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        // Get the Animator, Rigidbody, Renderer, and AudioSource components attached to the enemy
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();

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
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on " + gameObject.name);
        }

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

    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is the cube
        if (collision.gameObject.CompareTag("Cube"))
        {
            // Trigger the particle effect
            if (hitEffectPrefab != null)
            {
                Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            }

            // Increase the kill count using KillCountManager
            KillCountManager.instance.IncreaseKillCount();

            // Play the death sound
            if (deathSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(deathSound);
            }

            // Destroy the enemy after the sound has finished playing
            Destroy(gameObject, deathSound.length);

            Debug.Log("Enemy hit by the cube and destroyed!");
        }
    }

    IEnumerator RotatePeriodically()
    {
        while (true)
        {
            // Wait for 3 seconds
            yield return new WaitForSeconds(2f);

            // Rotate the enemy by a random angle between 90 and 270 degrees
            float randomAngle = Random.Range(90f, 270f);
            transform.Rotate(0, randomAngle, 0);
            Debug.Log("Enemy rotated by " + randomAngle + " degrees");
        }
    }
}
