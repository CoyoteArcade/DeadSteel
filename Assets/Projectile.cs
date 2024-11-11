using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;           // Speed of the projectile (base speed)
    public int damage = 10;             // Damage dealt by the projectile
    public float maxRange = 50f;        // Maximum distance the projectile can travel
    public float initialPlayerSpeed = 0f; // Speed inherited from the player

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // Record the initial position
    }

    void Update()
    {
        // Move the projectile forward at a speed that combines both the projectile speed and the player's speed
        float finalSpeed = speed + (initialPlayerSpeed * 0.2f); // Adjust this multiplier to control the effect of player speed
        transform.Translate(Vector3.forward * finalSpeed * Time.deltaTime);

        // Check the distance traveled
        float distanceTraveled = Vector3.Distance(startPosition, transform.position);
        if (distanceTraveled >= maxRange)
        {
            Destroy(gameObject); // Destroy the projectile if it exceeds max range
        }
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
        }
    }
}
