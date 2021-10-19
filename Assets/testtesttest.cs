using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HefimaLibrary;

public class testtesttest : MonoBehaviour
{
    public float moveSpeed;
    public float mouseSens;

    float turnSmoothVelocity;
    float turnSmoothTime = 0.1f;

    public GameObject player;
    public Rigidbody rb;
    public Transform cam;

    float xRotation = 0f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        LUL.CameraLook3D_FirstPerson(cam, player.transform, mouseSens, ref xRotation);
    }

    private void FixedUpdate()
    {
        //LUL.Move3D_RbTest(moveSpeed, player,rb,cam,ref turnSmoothVelocity,turnSmoothTime);
        LUL.Move3D_Rb(rb, cam, moveSpeed);
    }
}
