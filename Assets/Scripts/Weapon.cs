using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public Transform firePoint;         // Point where the projectile is spawned
    public float fireRate = 0.2f;       // Fire rate in seconds
    private float nextFireTime = 0f;

    private Movement playerMovement;    // Reference to player's movement script

    void Start()
    {
        playerMovement = GetComponentInParent<Movement>(); // Assumes Weapon is a child of the player
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate the projectile at the fire point's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Get the projectile script and pass the player's current speed to it
        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null && playerMovement != null)
        {
            projScript.initialPlayerSpeed = playerMovement.walkSpeed; // or use runSpeed if running
        }
    }
}
