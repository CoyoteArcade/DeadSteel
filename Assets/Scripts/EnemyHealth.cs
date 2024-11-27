using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Base attributes for the enemy
    public float baseHealth = 100f;
    public float baseDamage = 10f;

    // Scaling factors
    public float timeScalingFactor = 0.05f; // Health increase rate per second
    public float killScalingFactor = 5f;   // Health increase per kill
    public int killThreshold = 10;         // Kills needed for a buff

    private float currentHealth;
    private float currentDamage;
    private float elapsedTime;
    private int killCount;
    private float spawnTime;

    void Start()
    {
        spawnTime = Time.time;
        currentHealth = baseHealth;
        currentDamage = baseDamage;
    }

/*
    void Update()
    {
        elapsedTime = Time.time - spawnTime;

        // Gradually increase health over time
        float timeBonus = elapsedTime * timeScalingFactor;
        currentHealth = baseHealth + timeBonus;

        Debug.Log($"Enemy Stats - Health: {currentHealth}, Damage: {currentDamage}");
    }
*/
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
        killCount++;

        // Buff enemies after reaching the kill threshold
        if (killCount % killThreshold == 0)
        {
            BuffEnemies();
        }

        Destroy(gameObject);
        Debug.Log("Enemy destroyed!");
    }

    private void BuffEnemies()
    {
        baseHealth += killScalingFactor;
        baseDamage += killScalingFactor * 0.2f; // Damage scales slower than health

        Debug.Log($"Enemies buffed! New Base Health: {baseHealth}, Base Damage: {baseDamage}");
    }
}
