using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused; // Tracks the current pause state

    // Start is called before the first frame update
    void Start()
    {
        if (pauseMenu == null)
        {
            Debug.LogError("Pause Menu is not assigned in the Inspector.");
            return;
        }
        pauseMenu.SetActive(false); // Ensure the pause menu is hidden at the start
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor at the start
        Cursor.visible = false; // Hide the cursor at the start
        isPaused = false; // Ensure the game starts unpaused
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) // Check using the isPaused variable
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        if (isPaused) return; // Prevent multiple calls

        Debug.Log("Game Paused");
        isPaused = true; // Set pause state to true
        Time.timeScale = 0f; // Freeze time
        pauseMenu.SetActive(true); // Show pause menu
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Make the cursor visible
    }

    public void ResumeGame()
    {
        if (!isPaused) return; // Prevent multiple calls

        Debug.Log("Game Resumed");
        isPaused = false; // Set pause state to false
        Time.timeScale = 1f; // Resume time
        pauseMenu.SetActive(false); // Hide pause menu
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false; // Hide the cursor
    }

    public void ReturnToMenu()
    {
        Debug.Log("Returning to Menu");
        isPaused = false; // Reset pause state
        Time.timeScale = 1f; // Reset time scale before loading
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor for the menu
        Cursor.visible = true; // Make the cursor visible for the menu
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit(); // Quit the application
    }
}
