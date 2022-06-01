using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;                            // The player's starting health
    public int currentHealth;                                   // The  player's current health
    public Slider healthSlider;                                 // Reference to the UI's health bar
    public Image damageImage;                                   // Reference to an image to indicate the player being hurt
    public AudioClip deathClip;                                 // The sound to play when the player dies
    public float flashSpeed = 5f;                               // The speed the damageImage will fade
    public Color flashColour = new Color();                     // The colour of the damageImage

    Animator animator;                                          // Reference to the Animator
    AudioSource playerAudio;                                    // Reference to the AudioSource
    PlayerMovement playerMovement;                              // Reference to the player's movement script
    PlayerShooting playerShooting;                              // Reference to the PlayerShooting script
    bool isDead;                                                // Whether the player is dead
    bool damaged;                                               // Whether the player gets damaged


    void Awake()
    {
        // Setting up the references
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();

        // Set the health of the player
        currentHealth = startingHealth;
    }


    void Update()
    {
        // If the player has been damaged
        if (damaged)
        {
            // Set the colour of the damageImage
            damageImage.color = flashColour;
        }

        else
        {
            // Change the colour back
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }


    public void TakeDamage(int amount)
    {
        damaged = true;

        // Reduce the current health by the damage dealt
        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.Play();

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;

        playerShooting.DisableEffects();

        animator.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }
}
