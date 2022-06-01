using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public GameObject particleEffect;           // Reference to the particle system
    public AudioSource collectableSound;        // Reference to the AudioSource

    void OnTriggerEnter(Collider other)
    {
        // If the object tagged is collided with
        if (other.gameObject.CompareTag("Player"))
        {
            // Find the CollectableManager script and add the amount
            CollectableManager.collectableAmount += 1;
            gameObject.SetActive(false);
            Debug.Log("Point Collected");

            collectableSound.Play();
        }
    }

}