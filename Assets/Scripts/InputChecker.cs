using UnityEngine;

public class InputChecker : MonoBehaviour
{
    void Update()
    {
        // Detect if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed");
        }

        // Detect other keys
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key pressed");
        }

        // Detect mouse button clicks
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Debug.Log("Left mouse button clicked");
        }

        // Detect controller buttons (e.g., "Submit" or "Cancel" mapped in Input Settings)
        if (Input.GetButtonDown("Cancel"))
        {
            Debug.Log("Cancel button pressed");
        }
    }
}
