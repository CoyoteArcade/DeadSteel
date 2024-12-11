using System.Collections;
using UnityEngine;

namespace CombatSystem
{
    public class Enemy : MonoBehaviour
    {
        public int maxHealth = 30; // Set initial health for the enemy
        private int currentHealth; // Tracks current health
        private Animator animator; // Reference to the Animator component

        void Start()
        {
            // Initialize the enemy's health
            currentHealth = maxHealth;

            // Get the Animator component attached to this enemy or its children
            animator = GetComponentInChildren<Animator>();
            if (animator == null)
            {
                Debug.LogError($"Animator component not found on '{gameObject.name}' or its children. Please ensure it is properly attached.");
                DebugHierarchy();
            }
            else
            {
                Debug.Log($"Animator component found on '{animator.gameObject.name}' for enemy '{gameObject.name}'.");
            }
        }

        public void TakeDamage(int damage)
        {
            // Reduce health by the damage amount
            currentHealth -= damage;
            Debug.Log($"Enemy '{gameObject.name}' takes {damage} damage. Current health: {currentHealth}");

            // Check if health is zero or below, then trigger death animation
            if (currentHealth <= 0)
            {
                Debug.Log($"Enemy '{gameObject.name}' is dead! Triggering death animation.");
                // Trigger the death animation
                if (animator != null)
                {
                    animator.SetTrigger("isDead"); // Use the 'Die' trigger to play the death animation
                }

                // Optionally, you can delay the destruction of the enemy to allow the animation to play
                DestroyEnemy();
            }
        }

        private void DestroyEnemy()
        {
            // Destroy this GameObject after a short delay (to allow the death animation to play)
            Destroy(gameObject, 2f); // Adjust the delay based on the length of your death animation
            Debug.Log($"Enemy '{gameObject.name}' destroyed.");
        }

        private void DebugHierarchy()
        {
            Debug.Log($"Listing hierarchy for '{gameObject.name}':");
            foreach (Transform child in transform)
            {
                Debug.Log($"Child: {child.name}");
            }
        }
    }
}
