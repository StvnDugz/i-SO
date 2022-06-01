using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;                   // The target the enemy is to follow
    private Rigidbody rb;                       // Reference to the enemy rigidbody
    public float speed = 1f;                    // Reference to the enemy's movement speed
    public float rotationDamping = 0.1f;        // Reference to the enemy's rotation 


    void Start()
    {
        // Setting up the references
        target = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        Vector3 directionToTarget = target.position - transform.position;
        Quaternion newRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotationDamping);

        rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Player")
        {
            Debug.Log("Enemy Collided");
        }
    }
}