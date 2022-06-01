using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;       // Reference to the player's health
    public float restartDelay = 5f;			// Delay before restarting the level

    Animator anim;                          // Reference to the animator
    float restartTimer;     				// Timer before restarting the level


    void Awake()
    {
        // Set up the reference
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        // If the player has run out of health
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");

            restartTimer += Time.deltaTime;

            if (restartTimer >= restartDelay)
            {
            // Reload the currently loaded level
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}