using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public float attackCooldown = 1f;
    private float lastAttackTime;

    private Transform player;
    private Enemy enemy; // Reference to Enemy script for accessing stats

    void Start()
    {
        // Find the player GameObject (assumes the player has the "Player" tag)
        player = GameObject.FindWithTag("Player").transform;

        // Get the Enemy script attached to this GameObject
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        // Check if the player is within attack range and cooldown allows attacking
        if (Vector3.Distance(transform.position, player.position) <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        // Calculate damage based on elapsed time and critical hit chance
        float currentDamage = enemy.baseDamage + (Time.time * enemy.damageIncreaseRate);

        // Convert currentDamage (float) to int
        int damageToDeal = Mathf.RoundToInt(currentDamage);

        // Apply damage to the player's AttributesManager script
        player.GetComponent<AttributesManager>().TakeDamage(damageToDeal);

        Debug.Log($"Enemy dealt {damageToDeal} damage.");
    }
}
