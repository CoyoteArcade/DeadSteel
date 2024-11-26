using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem;

public class EnemyHealth : MonoBehaviour
{
    // Base attributes for the enemy
    public float baseHealth = 100f;
    public float baseDamage = 10f;
    public float baseSpeed = 5f;

    // Scaling rates
    public float healthIncreaseRate = 0.2f; // Rate of health increase over time
    public float maxHealthIncrease = 250f;  // Max additional health
    public float damageIncreaseRate = 0.1f; // Rate of damage increase
    public float speedIncreaseRate = 0.05f; // Rate of speed increase

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
        // Calculate elapsed time since spawn
        elapsedTime = Time.time - spawnTime;

        // Dynamically scale stats
        currentHealth = baseHealth + Mathf.Min(elapsedTime * healthIncreaseRate, maxHealthIncrease);
        currentDamage = baseDamage + (elapsedTime * damageIncreaseRate);
        currentSpeed = baseSpeed + (elapsedTime * speedIncreaseRate);

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
    }
}
