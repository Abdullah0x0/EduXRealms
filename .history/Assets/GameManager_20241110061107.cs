using UnityEngine;
using UnityEngine.UI; // For working with UI elements
using UnityEngine.SceneManagement; // For scene management
using TMPro;
using System.Collections; // Required for IEnumerator and coroutines

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the timer UI Text
    public TextMeshProUGUI countDownText; // Reference to the countdown UI Text
    public TextMeshProUGUI resultText; // Reference to the result UI Text
    public TextMeshProUGUI gameTitle;
    public int killCount = 0; // Number of monsters killed
    private float timer = 20f; // 30-second timer
    private bool isGameActive = false;

    void Start()
    {
        // Hide the timer, result text, and restart button at the beginning
        timerText.gameObject.SetActive(false);
        countDownText.gameObject.SetActive(false);
        resultText.gameObject.SetActive(false);

        // Start the countdown coroutine
        StartCoroutine(CountdownToStart());
    }

    // Coroutine to handle the countdown before the game starts
    private IEnumerator CountdownToStart()
    {
        countDownText.gameObject.SetActive(true);
        float countDownTime = 5.0f; // Countdown duration in seconds

        // Countdown loop
        while (countDownTime > 0)
        {
            countDownText.text = "Start in " + Mathf.Ceil(countDownTime).ToString() + "s";
            yield return new WaitForSeconds(1.0f); // Wait for 1 second
            countDownTime--;
        }

        countDownText.gameObject.SetActive(false);
        gameTitle.gameObject.SetActive(false);
        StartGame();
    }

    public void StartGame()
    {
        // Show the timer and set the game as active
        Debug.Log("Game is starting..."); // Debug message
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
            Debug.Log("Timer: " + timer); // Debug message to check if the timer is updating

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

        // Check win/loss condition
        if (killCount >= 10)
        {
            resultText.text = "WINNER!";
        }
        else
        {
            resultText.text = "GAME OVER - LOSS";
        }

        Debug.Log("Game Over. Kill Count: " + killCount); // Debug message for game over

        // Start the coroutine to reset the game after 5 seconds
        StartCoroutine(ResetGameAfterDelay());
    }

    // Coroutine to reset the game after a delay
    private IEnumerator ResetGameAfterDelay()
    {
        yield return new WaitForSeconds(5.0f); // Wait for 5 seconds
        RestartGame();
    }

    // Method to restart the game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
