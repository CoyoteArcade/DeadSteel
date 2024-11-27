using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f; // Speed of the enemy
    private Transform Player; // Reference to the player
    private Animator animator; // Reference to the Animator component

    void Start()
    {
        // Find the player GameObject by tag and get its Transform
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            Player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player GameObject not found. Make sure there is a GameObject tagged 'Player' in the scene.");
        }

        // Get the Animator component
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the enemy.");
        }
    }

    void Update()
    {
        if (Player == null)
        {
            if (animator != null)
            {
                animator.SetBool("isMoving", false);
            }
            return;
        }

        // Move towards the player
        Vector3 direction = (Player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);

        if (distanceToPlayer > 0.1f)
        {
            // Move the enemy and set "isMoving" animation to true
            transform.position += direction * speed * Time.deltaTime;
            if (animator != null)
            {
                animator.SetBool("isMoving", true);
            }

            // Smoothly rotate towards the player
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        else
        {
            // If the enemy isn't moving, set "isMoving" to false
            if (animator != null)
            {
                animator.SetBool("isMoving", false);
            }
        }
    }
}
