using UnityEngine;

public class KillTracker : MonoBehaviour
{
    public Weapon weapon; // Reference to the Weapon script

    void Start()
    {
        if (weapon == null)
        {
            Debug.LogError("Weapon script not assigned!");
        }
    }

    // Call this method whenever an enemy is killed
    public void OnEnemyKilled()
    {
        if (weapon != null)
        {
            weapon.OnEnemyKilled(); // This triggers the kill count and weapon upgrade
        }
    }
}
