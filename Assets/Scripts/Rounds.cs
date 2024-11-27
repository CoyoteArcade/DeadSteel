using UnityEngine;
using TMPro;

public class Rounds : MonoBehaviour
{
    private float elapsedTime;
    private int currentRound;
    private bool isRunning;

    [SerializeField] TMP_Text roundsLabel; // UI element to display timer and round info

    void Start()
    {
        elapsedTime = 0f;
        currentRound = 1; // Start at round 1
        isRunning = true; // Automatically start the timer
        UpdateRoundLabel(); // Initialize the label
    }

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;

            // Check if 20 seconds have passed for the current round
            if (elapsedTime >= currentRound * 20)
            {
                currentRound++;
                UpdateRoundLabel(); // Update the round text
                Debug.Log($"Round {currentRound} started at {FormatTime(elapsedTime)}");
            }

            // Update the timer text
            roundsLabel.text = $"Time: {FormatTime(elapsedTime)}  Current Round: {currentRound}";
        }
    }

    // Format the elapsed time into MM:SS:MS
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

        return $"{minutes:00}:{seconds:00}:{milliseconds:000}";
    }

    // Update the round label
    private void UpdateRoundLabel()
    {
        roundsLabel.text = $"Round {currentRound}";
    }

    // Public method to start the timer (optional for manual control)
    public void StartTimer()
    {
        isRunning = true;
        Debug.Log("Timer started.");
    }

    // Public method to stop the timer
    public void StopTimer()
    {
        isRunning = false;
        Debug.Log("Timer stopped.");
    }

    // Public method to reset the timer
    public void ResetTimer()
    {
        isRunning = false;
        elapsedTime = 0f;
        currentRound = 1;
        UpdateRoundLabel(); // Reset the label
        Debug.Log("Timer reset.");
    }
}
