using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;            // The enenmy's starting health
    public int currentHealth;                   // The enemy's current health
    public float sinkSpeed = 2.5f;              // The speed  that the enemy sinks through the floor when dead
    public int scoreValue = 20;                 // The amount added to the player's score when the enemy dies
    public AudioClip deathClip;                 // The sound to play when the enemy dies

    Animator anim;                              // Reference to the animator
    AudioSource enemyAudio;                     // Reference to the audio source
    ParticleSystem hitParticles;                // Reference to the particle system that plays when the enemy is hit
    CapsuleCollider capsuleCollider;            // Reference to the capsule collider
    bool isDead;                                // Whether the enemy is dead
    bool isDying;                               // Whether the enemy is dying


    void Awake()
    {
        // Setting up the references
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
    }


    void Update()
    {
        // If the enemy is sinking
        if (isDying)
        {
            // ... move the enemy down by the sinkSpeed per second.
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        // If the enemy is dead
        if (isDead)

            return;

        enemyAudio.Play();

        currentHealth -= amount;

        // Set the position of the particle system to where it was hit
        hitParticles.transform.position = hitPoint;

        hitParticles.Play();

        // If the current health is less than or equal to zero
        if (currentHealth <= 0)
        {
            Death();
        }
    }


    void Death()
    {
        // The enemy is dead
        isDead = true;

        // Turn the collider into a trigger so shots can pass through it
        capsuleCollider.isTrigger = true;

        // Tell the animator that the enemy is dead
        anim.SetTrigger("Dead");

        // Change the audio clip of the audio source to the death clip 
        enemyAudio.clip = deathClip;
        enemyAudio.Play();

        // Make the rigidbody kinematic
        GetComponent<Rigidbody>().isKinematic = true;

        isDying = true;

        // Increase the score by the enemy's score value.
        ScoreManager.score += scoreValue;

        Destroy(gameObject, 2f);

    }
    
}
