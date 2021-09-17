using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody rb;

    //Moving
    public float speed = 5f;
    public float runSpeed = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
}
