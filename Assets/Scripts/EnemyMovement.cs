using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f; // Speed of the enemy
    private Transform Player; // Reference to the player

    void Start()
    {
        // Find the player GameObject by tag and get its Transform
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            Player = playerObject.transform;
            // Start the movement coroutine
            StartCoroutine(MoveTowardsPlayer());
        }
        else
        {
            Debug.LogError("Player GameObject not found. Make sure there is a GameObject tagged 'Player' in the scene.");
        }
    }

    private System.Collections.IEnumerator MoveTowardsPlayer()
    {
        while (true) // Loop indefinitely
        {
            if (Player != null)
            {
                // Move towards the player
                Vector3 direction = (Player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
            }
            yield return null; // Wait until the next frame
        }
    }
}