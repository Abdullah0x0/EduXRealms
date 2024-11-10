using UnityEngine;
using UnityEngine.UI; // For working with UI elements
using UnityEngine.SceneManagement; // For scene management
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI countDownText; // Reference to the timer UI Text
    public TextMeshProUGUI timerText; // Reference to the timer UI Text
    public TextMeshProUGUI resultText; // Reference to the result UI Text
    public GameObject startButton; // Reference to the Start button
    public GameObject restartButton; // Reference to the Restart button
    public int killCount = 0; // Number of monsters killed
    private float timer = 30f; // 30-second timer
    private bool isGameActive = false;

    void Start()
    {
        // Hide the timer, result text, and restart button at the beginning
        countDownText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        resultText.gameObject.SetActive(false);
        restartButton.SetActive(false);

        countDownTimer();

        StartGame();
    }

    public void countDownTimer() {
        // Update the timer
        timer -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Ceil(timer).ToString() + "s";

        // Check if the timer has run out
        if (timer <= 0)
        {
            EndGame();
        }
    }

    public void StartGame()
    {
        // Hide the start button and show the timer
        countDownText.SetActive(false);
        startButton.SetActive(false);
        timerText.gameObject.SetActive(true);
        isGameActive = true;
    }

    void Update()
    {
        if (isGameActive)
        {
            // Update the timer
            timer -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Ceil(timer).ToString() + "s";

            // Check if the timer has run out
            if (timer <= 0)
            {
                EndGame();
            }
        }
    }

    public void IncreaseKillCount()
    {
        killCount++;
    }

    void EndGame()
    {
        isGameActive = false;
        timerText.gameObject.SetActive(false);
        resultText.gameObject.SetActive(true);
        restartButton.SetActive(true); // Show the restart button

        // Check win/loss condition
        if (killCount >= 10)
        {
            resultText.text = "WINNER!";
        }
        else
        {
            resultText.text = "GAME OVER - LOSS";
        }
    }

    // Method to restart the game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
