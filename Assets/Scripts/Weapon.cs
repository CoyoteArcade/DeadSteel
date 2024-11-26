using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Projectile Settings")]
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public Transform firePoint; // Point where the projectile is spawned

    [Header("Audio Settings")]
    public AudioSource audioSource; // Audio for shooting sound

    [Header("Weapon Upgrade Settings")]
    public GameObject[] weaponPrefabs; // Array of different weapon prefabs for upgrades
    public int[] weaponDamage; // Array to set different damage values for each weapon
    public float fireRate = 1f; // Default fire rate (seconds between shots)
    public int killsForSwap = 10; // Number of kills needed to upgrade or swap weapon

    private int killCount = 0; // Tracks the number of kills
    private int currentWeaponIndex = 0; // Tracks which weapon the player currently has
    private float nextFireTime = 0f; // Tracks when the next shot can be fired

    void Update()
    {
        // Check for firing input
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // Update next fire time
        }
    }

    void Shoot()
    {
        // Instantiate the projectile at the fire point's position and rotation
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Play shooting sound, if available
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    public void OnEnemyKilled()
    {
        // Increment kill count
        killCount++;
        Debug.Log($"Enemy killed! Total kills: {killCount}");

        // Check if it's time to upgrade the weapon
        if (killCount >= killsForSwap)
        {
            UpgradeWeapon();
            killCount = 0; // Reset kill count after upgrading
        }
    }

    void UpgradeWeapon()
    {
        // Decrease fire rate to shoot faster
        fireRate *= 0.8f; // Example: 20% faster fire rate
        Debug.Log($"Fire rate upgraded! New fire rate: {fireRate}");

        // Check if there is another weapon to switch to
        if (currentWeaponIndex < weaponPrefabs.Length - 1)
        {
            // Swap to the next weapon prefab
            currentWeaponIndex++;
            GameObject newWeapon = Instantiate(weaponPrefabs[currentWeaponIndex], firePoint.position, firePoint.rotation);
            newWeapon.transform.SetParent(transform);

            // Optionally, transfer fire point and audio source
            Weapon newWeaponScript = newWeapon.GetComponent<Weapon>();
            if (newWeaponScript != null)
            {
                newWeaponScript.firePoint = firePoint;
                newWeaponScript.audioSource = audioSource;
            }

            // Optionally, update projectile damage
            if (weaponDamage.Length > currentWeaponIndex)
            {
                Projectile projectileScript = projectilePrefab.GetComponent<Projectile>();
                if (projectileScript != null)
                {
                    projectileScript.damage = weaponDamage[currentWeaponIndex];
                }
            }

            Debug.Log($"Weapon upgraded to: {newWeapon.name}");
        }
        else
        {
            Debug.Log("Maximum weapon level reached!");
        }
    }
}
