using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem;

public class EnemyHealth : MonoBehaviour
{
<<<<<<< Updated upstream
    // Base stats for the enemy
=======
    // Base attributes for the Enemy
>>>>>>> Stashed changes
    public float baseHealth = 100f;
    public float baseDamage = 10f;
    public float baseSpeed = 5f;

<<<<<<< Updated upstream
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
=======
    // Scaling rates over time
    public float healthIncreaseRate = 0.2f; // Slower health increase rate
    public float maxHealthIncrease = 300f;  // Max health the enemy can reach

    private float elapsedTime;
    private float currentHealth;
    private float currentDamage;
    private float currentSpeed;

    private float spawnTime;

    void Start()
    {
        // Record the spawn time and initialize stats
        spawnTime = Time.time;
        currentHealth = baseHealth;
        currentDamage = baseDamage;
        currentSpeed = baseSpeed;
    }

    void Update()
    {
        // Calculate elapsed time since this enemy spawned
        elapsedTime = Time.time - spawnTime;

        // Dynamically scale stats, but cap the health increase
        currentHealth = baseHealth + Mathf.Min(elapsedTime * healthIncreaseRate, maxHealthIncrease);
        currentDamage = baseDamage + (elapsedTime * 0.1f); // Slow scaling of damage
        currentSpeed = baseSpeed + (elapsedTime * 0.05f);  // Slow scaling of speed

        Debug.Log($"Enemy Stats - Health: {currentHealth}, Damage: {currentDamage}, Speed: {currentSpeed}");
    }

    public void TakeDamage(float damage)
    {
        // Apply damage to the enemy
        currentHealth -= damage;

        Debug.Log($"Enemy took {damage} damage. Current Health: {currentHealth}");

        // Destroy enemy if health drops to 0 or below
        if (currentHealth <= 0)
        {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        Debug.Log("Enemy destroyed!");
        Destroy(gameObject);
>>>>>>> Stashed changes
    }
}
