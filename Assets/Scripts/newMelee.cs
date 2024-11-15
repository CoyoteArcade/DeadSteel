using System.Collections;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public float attackRange = 2.0f;
    public int attackDamage = 25;
    public float attackCooldown = 1.0f;

    [Header("Layer Settings")]
    public LayerMask enemyLayer;

    [Header("Sound Settings")]
    public AudioSource attackSound;  // Drag the AudioSource here in the Inspector

    private Animator animator;
    private float lastAttackTime = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= lastAttackTime + attackCooldown)
        {
            StartCoroutine(Attack());
            lastAttackTime = Time.time;
        }
    }

    private IEnumerator Attack()
    {
        // Play attack animation
        animator.SetTrigger("Attack");

        // Play attack sound
        if (attackSound != null)
        {
            attackSound.Play();
        }

        yield return new WaitForSeconds(0.3f); // Delay to sync with animation

        // Detect enemies in range
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position + transform.forward, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(attackDamage);
        }
    }

    // Optional: Visualize attack range in Scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward, attackRange);
    }
}
