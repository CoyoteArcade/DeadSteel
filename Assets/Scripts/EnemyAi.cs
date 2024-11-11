using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent; //reference agent

    public Transform Player;

    public LayerMask WhatIsPlayer;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        Player = GameObject.Find("PlayerObj").transform; //playerObj is what player is called; can be changed
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);

        // Always chase the player
        ChasePlayer();

        // Attack the player if in attack range
        if (playerInAttackRange) AttackPlayer();
    }

    private void ChasePlayer()
    {
        agent.SetDestination(Player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(Player); // When enemy attacks, looks at player

        // Check to see if it has not already attacked
        if (!alreadyAttacked)
        {
            // Attack code here... soon add melee attacks
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        // Implement damage logic here
    }

    private void DestroyEnemy()
    {
        // Implement destroy logic here
    }
}