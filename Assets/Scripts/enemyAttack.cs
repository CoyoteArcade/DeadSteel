using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem; // Correct namespace

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public float attackCooldown = 1f;
    private float lastAttackTime;

    private Transform player;
    private Enemy enemy;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Player GameObject not found! Ensure it has the 'Player' tag.");
        }

        enemy = GetComponent<Enemy>();

        if (enemy == null)
        {
            Debug.LogError("Enemy script not found on this GameObject!");
        }
    }

    void Update()
    {
        if (player != null && enemy != null)
        {
            if (Vector3.Distance(transform.position, player.position) <= attackRange && Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
    }

    void Attack()
    {
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
}
