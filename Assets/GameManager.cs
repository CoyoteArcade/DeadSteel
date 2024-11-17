using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    if (scene.name == "Map") // Adjust this to your Map Scene name
    {
        pauseMenuUI = GameObject.Find("PauseMenu"); // Ensure this matches the exact name in your Hierarchy
        if (pauseMenuUI == null)
        {
            Debug.LogError("Pause Menu UI not found in the Map Scene. Ensure it is named correctly.");
        }
    }
    else
    {
        pauseMenuUI = null; // Clear the reference in other scenes
    }
}
   public void PauseGame()
    {
    if (pauseMenuUI == null)
    {
        Debug.LogError("Pause Menu UI is not assigned or has been destroyed.");
        return; // Prevent further execution
    }
    pauseMenuUI.SetActive(true);
    Time.timeScale = 0f;
    isPaused = true;
    
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f; // Reset time scale
          isPaused = false; // Reset paused state
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false); // Hide pause menu
        }
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
