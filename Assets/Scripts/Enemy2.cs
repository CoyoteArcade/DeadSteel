using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public int maxHealth = 30; // Set initial health for the enemy
    private int currentHealth; // Tracks current health

    void Start()
    {
        // Initialize the enemy's health
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // Reduce health by the damage amount
        currentHealth -= damage;
        Debug.Log("Enemy takes " + damage + " damage. Current health: " + currentHealth);

        // Check if health is zero or below, then destroy the enemy
        if (currentHealth <= 0)
        {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        // Destroy this GameObject
        Destroy(gameObject);
        Debug.Log("Enemy destroyed");
    }
}
