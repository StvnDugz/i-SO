using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;                  // The damage of each bullet
    public float timeBetweenBullets = 0.25f;        // The time between each shot
    public float range = 100f;                      // The distance the bullet can reach


    float timer;                                    // A timer to determine when to fire
    Ray shootRay;                                   // A ray from the gun
    RaycastHit shootHit;                            // A raycast hit
    int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer
    ParticleSystem gunParticles;                    // Reference to the particle system
    LineRenderer gunLine;                           // Reference to the line renderer
    AudioSource gunAudio;                           // Reference to the audio source
    Light gunLight;                                 // Reference to the light
    float effectsDisplayTime = 0.25f;               // The effects displayed between bullets fired


    void Awake()
    {
        // Create a layer mask for the Shootable layer
        shootableMask = LayerMask.GetMask("Shootable");

        // Set up the references
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }


    void Update()
    {
        timer += Time.deltaTime;

        // If the Fire button is being press and it's time to fire
        if (Input.GetButtonDown("Fire1") && timer >= timeBetweenBullets)
        {
            Shoot();
        }

        // If the timer has exceeded the time between bullets display the effects
        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    void Shoot()
    {
        timer = 0f;

        gunAudio.Play();

        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        // Set the shootRay position
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        // If the raycast hits gameobjects on the shootable layer
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            // Find an EnemyHealth script on the gameobject hit
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                // Enemy takes damage
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }

            gunLine.SetPosition(1, shootHit.point);
        }

        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

}