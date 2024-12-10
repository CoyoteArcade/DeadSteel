using System.Collections;
using UnityEngine;

public class AttributesManager : MonoBehaviour
{
    // Public attributes for health, attack, and armor
    public int health;
    public int attack;
    public int armor;
    public float critDamage = 1.5f;
    public float critChance = 0.5f;

    // Method to take damage
    public void TakeDamage(int amount)
    {
        int damageTaken = Mathf.Max(amount - (amount * armor / 100), 1); // Ensure at least 1 damage
        health -= damageTaken;
        health = Mathf.Max(health, 0); // Prevent health from dropping below 0

        Debug.Log($"{gameObject.name} took {damageTaken} damage. Remaining health: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    // Method to deal damage to another GameObject
    public void DealDamage(GameObject target)
    {
        var targetAttributes = target.GetComponent<AttributesManager>();
        if (targetAttributes != null)
        {
            float totalDamage = attack;

            // Calculate critical hit
            if (Random.Range(0f, 1f) < critChance)
            {
                totalDamage *= critDamage;
                Debug.Log("Critical Hit!");
            }

            Debug.Log($"{gameObject.name} is dealing {Mathf.RoundToInt(totalDamage)} damage to {target.name}");
            targetAttributes.TakeDamage(Mathf.RoundToInt(totalDamage));
        }
    }

    // Method to handle death
    private void Die()
    {
        if (gameObject.name == "Player")
        {
            HandlePlayerDeath();
        }
        else
        {
            HandleEnemyDeath();
        }
    }

    // Handle player death
    private void HandlePlayerDeath()
    {
        Debug.Log("Player has died!");
        // Trigger Game Over UI or respawn logic
        StartCoroutine(RespawnPlayer());
    }

    // Handle enemy death
    private void HandleEnemyDeath()
    {
        Debug.Log($"{gameObject.name} (Enemy) has died!");
        // Play enemy death animation or particle effects here
        Destroy(gameObject, 1.5f); // Optional delay for death effects
    }

    // Example coroutine for player respawn
    private IEnumerator RespawnPlayer()
    {
        Debug.Log("Respawning player...");
        // Hide player or disable controls
        gameObject.SetActive(false);

        // Wait for respawn time
        yield return new WaitForSeconds(3f);

        // Reset health and reposition player
        health = 100; // Default health value
        transform.position = Vector3.zero; // Example respawn position
        Debug.Log("Player respawned!");

        // Reactivate player
        gameObject.SetActive(true);
    }
}
