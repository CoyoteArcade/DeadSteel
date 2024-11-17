using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu; // Assign the Pause Menu UI Canvas in the Inspector
    private bool isPaused = false; // Tracks the current pause state

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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
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

        isPaused = true; // Set pause state to true
        Time.timeScale = 0f; // Freeze time
        pauseMenu.SetActive(true); // Show pause menu
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Make the cursor visible
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        isPaused = false; 
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
