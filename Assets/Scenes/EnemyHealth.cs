using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyHealth = 100; // Set the initial health of the enemy

    // Update method to simulate damage for testing purposes
    void Update()
    {
        if (enemyHealth <= 0)
        {
            DestroyEnemy();
        }
    }

    // Method to apply damage to the enemy
    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            DestroyEnemy();
        }
    }

    // Method to destroy the enemy when health reaches 0
    private void DestroyEnemy()
    {
        Destroy(gameObject);
        Debug.Log("Enemy destroyed");
    }
}

