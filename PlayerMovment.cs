using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public CharacterController controller;
    
    public float speed = 12f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Initialize y speed each time you are on the ground
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Footsteps
        if (controller.velocity.magnitude > 0.3f && !GetComponent<AudioSource>().isPlaying && isGrounded)
        {
            GetComponent<AudioSource>().volume = Random.Range(0.5f, 1f);
            GetComponent<AudioSource>().pitch = Random.Range(0.5f, 1.1f);
            GetComponent<AudioSource>().Play();
        }

        // Movment
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
