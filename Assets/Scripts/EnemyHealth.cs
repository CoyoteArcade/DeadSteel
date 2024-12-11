using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Base attributes for enemies
    public static float baseHealth = 100f;
    public static float baseDamage = 10f;
    public static float baseSpeed = 2f; // Movement speed of the enemy

    // Buffing factors
    public static int buffThreshold = 5; // Eliminations needed for a buff

    private float currentHealth;
    private float currentDamage;
    private float currentSpeed;

    // Global kill count shared across all enemies
    private static int globalKillCount = 0;

    private void Start()
    {
        // Set current stats to base values
        currentHealth = baseHealth;
        currentDamage = baseDamage;
        currentSpeed = baseSpeed;

        Debug.Log($"New enemy spawned with Health: {currentHealth}, Damage: {currentDamage}, Speed: {currentSpeed}");
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        Debug.Log($"Enemy took {damage} damage. Current Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        globalKillCount++;

        // Buff enemies when reaching the buff threshold
        if (globalKillCount % buffThreshold == 0)
        {
            BuffEnemies();
        }

        Destroy(gameObject);
        Debug.Log("Enemy destroyed!");
    }

    private void BuffEnemies()
    {
        // Double the base attributes
        baseHealth *= 2;
        baseDamage *= 2;
        baseSpeed *= 2;

        Debug.Log($"Enemies buffed! New Base Health: {baseHealth}, Base Damage: {baseDamage}, Base Speed: {baseSpeed}");
    }
}
