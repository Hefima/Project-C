using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testcamshit : MonoBehaviour
{
    public Transform cam;

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0f, cam.eulerAngles.y, 0f);
    }
}
