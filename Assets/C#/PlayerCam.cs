using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public Transform cam;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;


    public float targetAngle()
    {
        float targetAngle = Mathf.Atan2(PlayerManager.acc.IK.AxisX, PlayerManager.acc.IK.AxisY) * Mathf.Rad2Deg + cam.eulerAngles.y;

        return targetAngle;
    }

    public float angle()
    {
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle(), ref turnSmoothVelocity, turnSmoothTime);

        return angle;
    }
}
