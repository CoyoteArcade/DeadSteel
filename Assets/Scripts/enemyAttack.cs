using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem; // Correct namespace

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 2f; // Range within which the enemy can attack
    public float attackCooldown = 1f; // Time between consecutive attacks
    private float lastAttackTime;

    private Transform player; // Reference to the player's Transform
    private Enemy enemy; // Reference to the Enemy script
    private Animator animator; // Reference to the Animator component

    void Start()
    {
        // Find the player by tag
        player = GameObject.FindWithTag("Player").transform;
        if (player == null)
        {
            Debug.LogError("Player GameObject not found! Ensure it has the 'Player' tag.");
        }

        // Get the Enemy component
        enemy = GetComponent<Enemy>();
        if (enemy == null)
        {
            Debug.LogError("Enemy script not found on this GameObject!");
        }

        // Get the Animator component
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on this GameObject!");
        }
    }

    void Update()
    {
        if (player != null && enemy != null)
        {
            // Check if the player is within attack range and if the attack cooldown has elapsed
            if (Vector3.Distance(transform.position, player.position) <= attackRange && Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
    }

    void Attack()
    {
        // Trigger the attack animation
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // Deal damage to the player after a short delay (sync with the animation)
        StartCoroutine(DealDamageAfterDelay(0.5f)); // Adjust delay to match the animation timing
    }

    private IEnumerator DealDamageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        int damageToDeal = Mathf.RoundToInt(enemy.maxHealth * 0.1f); // Example: 10% of max health as damage

        var attributesManager = player.GetComponent<AttributesManager>();
        if (attributesManager != null)
        {
            attributesManager.TakeDamage(damageToDeal);
            Debug.Log($"Enemy dealt {damageToDeal} damage.");
        }
        else
        {
            Debug.LogError("Player does not have an AttributesManager component!");
        }
    }

    // Optional: Visualize the attack range in the Scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
