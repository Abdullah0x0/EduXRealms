using UnityEngine;
using TMPro; // Include TextMeshPro namespace for VR-friendly text rendering

public class KillCountManager : MonoBehaviour
{
    public static KillCountManager instance; // Singleton instance for easy access
    public int killCount = 0; // The total number of monsters killed
    public TextMeshProUGUI killCountText; // Reference to the TextMeshPro UI element

    void Awake()
    {
        // Ensure there is only one instance of KillCountManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to increase the kill count and update the UI
    public void IncreaseKillCount()
    {
        killCount++;
        UpdateKillCountUI();
    }

    // Method to update the UI
    private void UpdateKillCountUI()
    {
        killCountText.text = "Kills: " + killCount;
    }
}
