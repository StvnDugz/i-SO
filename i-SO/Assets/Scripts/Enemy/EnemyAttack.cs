using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;     // The time between each attack
    public int attackDamage = 10;               // The damage of each attack

    Animator animator;                          // Reference to the Animator
    GameObject player;                          // Reference to the player game object
    PlayerHealth playerHealth;                  // Reference to the player health
    EnemyHealth enemyHealth;                    // Reference to the enemy health
    bool playerInRange;                         // Whether the player is in range
    float timer;                                // A timer to determine when to attack


    void Awake()
    {
        // Setting up the references
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack();
        }

        if (playerHealth.currentHealth <= 0)
        {
            animator.SetTrigger("PlayerDead");
        }
    }


    void OnTriggerEnter(Collider other)
    {
        // If the player is within range to attack
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Attack()
    {
        timer = 0f;

        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
