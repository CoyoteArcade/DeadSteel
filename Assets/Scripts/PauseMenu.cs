using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        if (pauseMenu == null)
        {
            Debug.LogError("Pause Menu is not assigned in the Inspector.");
            return;
        }
        pauseMenu.SetActive(false); // Ensure it's hidden at start
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0f) // Check actual time scale
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
        Debug.Log("Game Paused");
        Time.timeScale = 0f; // Freeze time
        pauseMenu.SetActive(true); // Show pause menu
        isPaused = true;
    }

    public void ResumeGame()
    {
        Debug.Log("Game Resumed");
        Time.timeScale = 1f; // Resume time
        pauseMenu.SetActive(false); // Hide pause menu
        isPaused = false;
    }

    public void ReturnToMenu()
    {
        Debug.Log("Returning to Menu");
        Time.timeScale = 1f; // Reset time scale before loading
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
