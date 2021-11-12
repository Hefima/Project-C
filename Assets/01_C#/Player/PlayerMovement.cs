using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController con;
    public Transform cam;
    public Animator anim;

    public float groundedGravity = -0.5f;
    public float gravity = -9.81f;

    Vector2 moveDir;
    Vector3 walking;
    Vector3 running;

    Vector3 velocity;

    public float speed = 5f;
    public float runSpeed = 10f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // Jumping
    public bool isJumpPressed = false;
    bool isJumping = false;
    float initialJumpVelocity;
    float maxJumpHeight = 1f;
    float maxJumpTime = 0.5f;

    private void Awake()
    {
        SetupJumpVariables();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (input.magnitude >= 0.1f)
        {
            anim.SetBool("isWalking", true);

            float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = new Vector2(transform.forward.x, transform.forward.z);

            walking.x = moveDir.x * speed;
            walking.z = moveDir.y * speed;
            
            running.x = moveDir.x * runSpeed;
            running.z = moveDir.y * runSpeed;
        }
        else
        {
            moveDir = Vector2.zero;
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("isRunning", true);
            con.Move(running * Time.deltaTime);
        }
        else
        {
            anim.SetBool("isRunning", false);
            con.Move(walking * Time.deltaTime);
        }

        velocity.y += gravity;

        con.Move(velocity * Time.deltaTime);
        Inputs();

        //Jump();

        //myGravity();
    }

    void myGravity()
    {
        if (con.isGrounded)
        {
            walking.y += groundedGravity;
            running.y += groundedGravity;
        }
        else
        {
            walking.y += gravity * Time.deltaTime;
            running.y += gravity * Time.deltaTime;
        }
    }

    void SetupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        //gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    void Jump()
    {
        if(!isJumping && con.isGrounded && isJumpPressed)
        {
            print("jump");
            isJumping = true;
            anim.SetBool("isJumping", true);
            //moveDir.y = initialJumpVelocity;
        } else if (!isJumpPressed && isJumping && con.isGrounded)
        {
            anim.SetBool("isJumping", false);
            isJumping = false;
        }
    }

    void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpPressed = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumpPressed = false;
        }
    }
}
