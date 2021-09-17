using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    public CharacterController controller;
    public Animator anim;

    //Move
    Vector3 move;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public bool moveAllowed = true;

    //Jump
    public bool jumpPressed = false;
    bool isJumping = false;
    float initialJumpVelocity;
    public float maxJumpHeight = 1f;
    public float maxJumpTime = 0.5f;
    //public float jumpHeight = 2f;
    bool isFalling;
    float fallTime;

    //Gravity
    Vector3 velocity;
    public float gravity = -10f;

    //GroundCheck
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;




    public Transform cam;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private void Awake()
    {
        SetupJumpVariables();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        jumpPressed = Input.GetKeyDown(KeyCode.Space);


        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (input.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            move = cam.forward * input.y + cam.right * input.x;
            //Vector2 move = new Vector2(transform.forward.x, transform.forward.z);

            if (Input.GetKey(KeyCode.LeftShift) && isGrounded && moveAllowed)
            {
                anim.SetBool("isRunning", true);
                controller.Move(move * runSpeed * Time.deltaTime);
            }
            else if(moveAllowed)
            {
                if(isGrounded)
                    anim.SetBool("isWalking", true);

                controller.Move(move * walkSpeed * Time.deltaTime);
            }
        }
        else
        {
            move = Vector3.zero;
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);           
        }


        /*if (jumpPressed && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }*/

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


        if(!isGrounded && !isJumping)
        {
            if (!isFalling)
            {
                fallTime = Time.time + 0.2f;
                isFalling = true;
            }

            if(!isGrounded && !isJumping && fallTime <= Time.time)
            {
                anim.SetBool("isFalling", true);
                isFalling = false;
            }

        }
        else
        {
            anim.SetBool("isFalling", false);
            isFalling = false;
        }

        Jump();
    }
    void SetupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;


    }
    void Jump()
    {
        if (!isJumping && isGrounded && jumpPressed)
        {
            print("jump");
            isJumping = true;
            velocity.y = initialJumpVelocity;
            anim.SetBool("isJumping", true);
        }
        else if (!jumpPressed && isJumping && isGrounded)
        {
            anim.SetBool("isJumping", false);
            isJumping = false;
        }
    }
}
