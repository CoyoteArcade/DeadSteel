using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;   // Speed of the projectile
    public int damage = 10;     // Damage dealt by the projectile
    private static int killCount = 0; // Track number of kills globally

    void Update()
    {
        // Move the projectile forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the projectile hits an enemy
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Apply damage to the enemy
            }
            Destroy(gameObject); // Destroy the projectile on impact

            // Increment kill count after the projectile hits an enemy
            OnEnemyKilled();
        }
    }

    // Method to track kills and apply upgrades
    private void OnEnemyKilled()
    {
        killCount++; // Increase kill count

        if (killCount >= 10) // After 10 kills
        {
            // Increase projectile speed and damage
            speed += 10f;  // Increase speed by 10
            damage += 5;   // Increase damage by 5

            Debug.Log("Projectile speed and damage increased!");
            killCount = 0;  // Reset kill count after upgrade
        }
    }
}
