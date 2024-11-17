using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public Transform firePoint; // Point where the projectile is spawned

    void Update()
    {
        // Prevent shooting when the game is paused
        if (Time.timeScale == 0f)
        {
            return;
        }

        // Handle shooting input
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate the projectile at the fire point's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Debug.Log("Projectile fired!");
    }
}
