using UnityEngine;
using UnityEngine.AI;

public class CombinedEnemyAI : MonoBehaviour
{
    // Components
    public NavMeshAgent agent;
    private Rigidbody rb;

    // Target
    public Transform player;

    // Layers
    public LayerMask whatIsGround, whatIsPlayer;

    // Health
    public float health;

    // Patroling / Roaming
    public Vector3 roamingTarget;
    public float walkPointRange = 10.0f;
    public float roamingSpeed = 2.0f;
    bool walkPointSet;

    // Chasing
    public float chasingSpeed = 5.0f;

    // Attacking
    public GameObject projectile;
    public float timeBetweenAttacks;
    private bool alreadyAttacked;

    // Detection Ranges
    public float sightRange, attackRange;
    public float obstacleAvoidanceDistance = 1.0f;
    private bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        if (player == null)
            Debug.LogWarning("Player not found in the scene.");

        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        if (rb == null)
            Debug.LogError("Rigidbody component is missing from this GameObject.");
    }

    private void Update()
    {
        if (player == null)
            return;

        // Check for player in sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SetNewRoamingTarget();

        if (walkPointSet)
        {
            MoveTowards(roamingTarget, roamingSpeed);

            if (Vector3.Distance(transform.position, roamingTarget) < 0.5f)
                walkPointSet = false;
        }
    }

    private void SetNewRoamingTarget()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        roamingTarget = new Vector3(
            transform.position.x + randomX,
            transform.position.y,
            transform.position.z + randomZ
        );

        if (Physics.Raycast(roamingTarget, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.speed = chasingSpeed;
        MoveTowards(player.position, chasingSpeed);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // Attack logic
            Rigidbody rbProjectile = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rbProjectile.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rbProjectile.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void MoveTowards(Vector3 target, float speed)
    {
        Vector3 direction = (target - transform.position).normalized;
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;

        if (!Physics.Raycast(transform.position, direction, obstacleAvoidanceDistance))
        {
            rb.MovePosition(newPosition);
        }
        else
        {
            Vector3 avoidanceDirection = Vector3.Cross(direction, Vector3.up).normalized;
            newPosition = transform.position + avoidanceDirection * speed * Time.deltaTime;
            rb.MovePosition(newPosition);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
