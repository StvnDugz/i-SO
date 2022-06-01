using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{                                                          
    private CharacterController player;             // Reference to the player character comtroller
    private Animator animator;                      // Reference to the Animator
    bool jump;                                      // Whether the player has jumped
    bool isWalking;
    //bool crouch = false;                          // Whether the player has crouched

    public float moveSpeed;                         // The player's movement speed
    public float jumpForce;                         // The player's jump force
    public float gravityScale;                      // The strenght of the gravity
    Vector3 moveDirection;


    void Start()
    {
        // Setting up the references
        player = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {

        moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);

        // If the player is grounded
        if (player.isGrounded)
        {

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                animator.SetBool("IsWalking", true);
            }

            else
            {
                animator.SetBool("IsWalking", false);
            }

            // If the jump button is pressed and the player is grounded
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
                //animator.SetTrigger("Jump");
                jump = true;
                print("jump button was pressed");
            }




        }

        // Set the camRay so that it points to the mouse position
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float camRayLenght;

        if (groundPlane.Raycast(camRay, out camRayLenght))
        {
            Vector3 pointToLook = camRay.GetPoint(camRayLenght);
            Debug.DrawLine(camRay.origin, pointToLook, Color.blue);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

    }


    void FixedUpdate()
    {
        Move();
    }


    void Move()
    {
        // Move our character
        player.Move(moveDirection * Time.deltaTime);
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        jump = false;

    }
}