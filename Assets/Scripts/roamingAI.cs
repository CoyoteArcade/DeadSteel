using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roamingAI : MonoBehaviour
{
    public float roamingSpeed = 2.0f;
    public float chasingSpeed = 5.0f;
    public float detectionRange = 10.0f;
    public Transform player;
    public float obstacleAvoidanceDistance = 1.0f;

    private Vector3 roamingTarget;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing from this GameObject");
            return;
        }

        if (player == null)
        {
            GameObject playerObject = GameObject.Find("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogWarning("Player Transform is not assigned in the inspector and no GameObject named 'Player' was found in the scene.");
            }
        }
        SetNewRoamingTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Player Transform is not assigned.");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            // Chase the player
            ChasePlayer();
        }
        else
        {
            // Roam around
            Roam();
        }
    }

    void SetNewRoamingTarget()
    {
        roamingTarget = new Vector3(
            transform.position.x + Random.Range(-10.0f, 10.0f),
            transform.position.y,
            transform.position.z + Random.Range(-10.0f, 10.0f)
        );
    }

    void Roam()
    {
        MoveTowards(roamingTarget, roamingSpeed);

        if (Vector3.Distance(transform.position, roamingTarget) < 0.5f)
        {
            SetNewRoamingTarget();
        }
    }

    void ChasePlayer()
    {
        MoveTowards(player.position, chasingSpeed);
    }

    void MoveTowards(Vector3 target, float speed)
    {
        Vector3 direction = (target - transform.position).normalized;
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;

        // Obstacle avoidance
        if (!Physics.Raycast(transform.position, direction, obstacleAvoidanceDistance))
        {
            rb.MovePosition(newPosition);
        }
        else
        {
            // Simple obstacle avoidance by changing direction
            Vector3 avoidanceDirection = Vector3.Cross(direction, Vector3.up).normalized;
            newPosition = transform.position + avoidanceDirection * speed * Time.deltaTime;
            rb.MovePosition(newPosition);
        }
    }
}