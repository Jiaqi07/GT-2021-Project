using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    Vector3 moveObject;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        //Left click to fly up
        if (Input.GetMouseButtonDown(0))
        {
            if (velocity.y < 0)
            {
                controller.Move(-velocity * Time.deltaTime * 75);
                
            }
            else if (velocity.y > 0)
            {
                controller.Move(velocity * Time.deltaTime * 75);
                controller.Move(velocity * Time.deltaTime * z);
            }

        }
        else if (!Input.GetMouseButtonDown(0))
        {
            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
        
        controller.Move(move * speed * Time.deltaTime);   
        
    }

}
