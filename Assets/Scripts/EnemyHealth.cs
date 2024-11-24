using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Base stats for the enemy
    public float baseHealth = 100f;
    public float baseDamage = 10f;
    public float baseSpeed = 5f;

    // Scaling rates
    public float healthIncreaseRate = 2f;
    public float damageIncreaseRate = 0.5f;
    public float speedIncreaseRate = 0.1f;

    private float elapsedTime;

    void Update()
    {
        // Update elapsed time
        elapsedTime = Time.time;

        // Update stats dynamically (not strictly necessary here if only referenced elsewhere)
        float currentHealth = baseHealth + (elapsedTime * healthIncreaseRate);
        float currentDamage = baseDamage + (elapsedTime * damageIncreaseRate);
        float currentSpeed = baseSpeed + (elapsedTime * speedIncreaseRate);

        Debug.Log($"Enemy Stats - Health: {currentHealth}, Damage: {currentDamage}, Speed: {currentSpeed}");
    }
}

