using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;
    public Animator anim;

    //Move
    public Vector2 input;
    Vector3 move;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public bool moveAllowed = true;

    //Jump
    public bool jumpPressed = false;
    public bool isJumping;
    public float initialJumpVelocity;
    public float maxJumpHeight = 1f;
    public float maxJumpTime = 0.5f;

    public bool isFalling;
    float fallTime;

    //Gravity
    public Vector3 velocity;
    public float gravity = -10f;

    //GroundCheck
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;

    //Cam
    public GameObject cam;
    public GameObject moveRotation;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    void Awake()
    {
        SetupJumpVariables();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PlayerMoveUpdate()
    {
        GroundCheck();
        Jump();
        moveRotation.transform.eulerAngles = new Vector3(0f, cam.transform.eulerAngles.y, 0f);
    }
    public void PlayerMoveFixedUpdate()
    {
        Gravity();
        Move();
    }

    void Gravity()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    void Move()
    {
        input = new Vector2(GameManager.acc.IK.AxisX, GameManager.acc.IK.AxisY).normalized;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        if (input.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                        
            move = moveRotation.transform.forward * input.y + moveRotation.transform.right * input.x;

            if (GameManager.acc.IK.input_Shift && isGrounded && moveAllowed)
            {
                anim.SetBool("isRunning", true);
                controller.Move(move * (PlayerManager.acc.livePlayerStats.agility * 2) * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
            else if (moveAllowed)
            {
                if (isGrounded)
                    anim.SetBool("isWalking", true);

                controller.Move(move * PlayerManager.acc.livePlayerStats.agility * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }
        else if(!moveAllowed ||  input.magnitude <= 0.1)
        {
            StopWalking();
        }

        controller.Move(velocity * Time.deltaTime);
    }

    public void StopWalking()
    {
        move = Vector3.zero;
        anim.SetBool("isRunning", false);
        anim.SetBool("isWalking", false);
    }

    void SetupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    void Jump()
    {
        if (GameManager.acc.IK.input_Space && !isJumping && isGrounded)
        {
            isJumping = true;
            velocity.y = initialJumpVelocity;
            anim.SetBool("isJumping", true);
            anim.SetTrigger("jump");
        }
        else if (!GameManager.acc.IK.input_Space && isJumping && isGrounded)
        {
            anim.SetBool("isJumping", false);
            isJumping = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
    }
}
