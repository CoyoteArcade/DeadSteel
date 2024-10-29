using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f; // Speed of the enemy
    private Transform player; // Reference to the player

    void Start()
    {
        // Find the player GameObject by tag and get its Transform
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        // Start the movement coroutine
        StartCoroutine(MoveTowardsPlayer());
    }

    private System.Collections.IEnumerator MoveTowardsPlayer()
    {
        while (true) // Loop indefinitely
        {
            if (player != null)
            {
                // Move towards the player
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
            }
            yield return null; // Wait until the next frame
        }
    }
}
