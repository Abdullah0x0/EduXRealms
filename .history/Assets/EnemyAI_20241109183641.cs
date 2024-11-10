using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float runSpeed = 2.0f; // Speed at which the enemy runs away
    public Transform playerHead;  // Reference to the player's head (CenterEyeAnchor)
    public float detectionAngle = 90f; // Angle threshold for detecting player's gaze

    void Update()
    {
        // Calculate the direction from the enemy to the player's head
        Vector3 directionToPlayer = playerHead.position - transform.position;

        // Modify the player's forward direction to ignore the vertical component
        Vector3 gazeDirection = new Vector3(playerHead.forward.x, 0, playerHead.forward.z).normalized;

        // Calculate the angle between the modified gaze direction and the direction to the player
        float angle = Vector3.Angle(gazeDirection, directionToPlayer);

        // Debug log to see the angle in the Unity console
        Debug.Log("Angle to player: " + angle);

        // // If the player is looking at the enemy (within the detection angle)
        // if (angle < detectionAngle)
        // {
        //     Debug.Log("Player is looking at me!");
        //     RunAway();
        // }

        RunAway();
    }

    void RunAway()
    {
        // Calculate the direction to run away from the player
        Vector3 runDirection = (transform.position - playerHead.position).normalized;

        // Move the enemy away from the player at the specified speed
        transform.position += runDirection * runSpeed * Time.deltaTime;
    }
}
