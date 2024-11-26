using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public Transform firePoint; // Point where the projectile is spawned
<<<<<<< Updated upstream
=======
    public AudioSource audioSource; // Audio for shooting sound
    public GameObject[] weaponPrefabs; // Array of different weapon prefabs (for upgrades)
    public int[] weaponDamage; // Optional array to set different damage values for each weapon

    public float fireRate = 1f; // Default fire rate (seconds between shots)
    private float nextFireTime = 0f; // Tracks when the next shot can be fired

    private int killCount = 0; // Tracks number of kills
    private int killsForSwap = 10; // Number of kills needed to swap weapons
    private int currentWeaponIndex = 0; // Tracks which weapon the player currently has
>>>>>>> Stashed changes

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate the projectile at the fire point's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}

<<<<<<< Updated upstream
=======
    // Call this method when an enemy is killed
    public void OnEnemyKilled()
    {
        killCount++;
        Debug.Log("Enemy killed! Total kills: " + killCount);

        // If the kill count reaches the threshold, upgrade or swap the weapon
        if (killCount >= killsForSwap)
        {
            UpgradeWeapon();
            killCount = 0; // Reset kill count after upgrading
        }
    }

    // Swaps the weapon or upgrades fire rate when the kill count is reached
    void UpgradeWeapon()
    {
        // Decrease fire rate (reduce time between shots, making it faster)
        fireRate *= 0.8f; // For example, 20% faster fire rate (0.8 means faster)
        Debug.Log("Fire rate upgraded! New fire rate: " + fireRate);

        // Check if there is a new model to switch to
        if (currentWeaponIndex < weaponPrefabs.Length - 1)
        {
            // Destroy the old weapon (make sure the current weapon is destroyed before instantiating the new one)
            Debug.Log("Destroying old weapon: " + gameObject.name);
            Destroy(gameObject);

            // Instantiate the next weapon
            currentWeaponIndex++; // Move to the next weapon
            GameObject newWeapon = Instantiate(weaponPrefabs[currentWeaponIndex], firePoint.position, firePoint.rotation);

            // Log the instantiation to verify the weapon is being spawned
            Debug.Log("Instantiating new weapon: " + newWeapon.name + " at position: " + firePoint.position);

            // Set the new weapon as a child of the player (or main character)
            newWeapon.transform.SetParent(transform);

            // Optionally, assign the firePoint and audioSource if your new weapon doesn't have them
            Weapon newWeaponScript = newWeapon.GetComponent<Weapon>();
            if (newWeaponScript != null)
            {
                newWeaponScript.firePoint = firePoint;
                newWeaponScript.audioSource = audioSource;
            }

            // Optionally, set new damage values based on the weapon level
            if (weaponDamage.Length > currentWeaponIndex)
            {
                newWeapon.GetComponent<Projectile>().damage = weaponDamage[currentWeaponIndex];
            }

            // Log the new weapon being swapped
            Debug.Log("Weapon swapped to: " + newWeapon.name + " with new fire rate: " + fireRate);
        }
        else
        {
            Debug.Log("Max weapon level reached!");
        }
    }
}
>>>>>>> Stashed changes
