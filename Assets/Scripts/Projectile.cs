using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 40f;   // Speed of the projectile
    public int damage = 10;     // Damage dealt by the projectile

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
        }
    }
}