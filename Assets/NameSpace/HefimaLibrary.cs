using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HefimaLibrary
{
    public class HefiMath : MonoBehaviour
    {
        public static Vector3 RandomVector3_Plane(float radius, Vector3 middlePoint)
        {
            return new Vector3(Random.Range(-radius, radius) + middlePoint.x, middlePoint.y, Random.Range(-radius, radius) + middlePoint.z);
        }

        public static Vector3 RandomVector3(float radius, Vector3 middlePoint)
        {
            return new Vector3(Random.Range(-radius, radius) + middlePoint.x, Random.Range(-radius, radius) + middlePoint.y, Random.Range(-radius, radius) + middlePoint.z);
        }

        /*public static Vector3 QuadraticBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            return;
        }*/

        public static Vector3 CubicBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            return (((-p0 + 3 * (p1 - p2) + p3) * t + (3 * (p0 + p2) - 6 * p1)) * t + 3 * (p1 - p0)) * t + p0;
        }
    }


    public class LUL : MonoBehaviour
    {
        public static void Move2D_Rb(float movementSpeed, Rigidbody2D rb)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime, rb.velocity.y);
        }

        public static void Move3D_RbTest(float movementSpeed, GameObject player,Rigidbody rb, Transform cam, ref float turnSmoothVelocity, float turnSmoothTime)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            if(input.magnitude > 0.1f)
            {
                

                float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                player.transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 move = cam.forward * input.y + cam.right * input.x;

                rb.velocity = move * movementSpeed;
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }

        public static void Move3D_Rb(Rigidbody rigidbody, Transform cam, float movementSpeed)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            if(input.magnitude > 0.1f)
            {
                Vector3 move = cam.forward * input.y + cam.right * input.x;

                rigidbody.velocity = move * movementSpeed;
            }
            else
            {
                rigidbody.velocity = Vector3.zero;
            }
        }

        public static void CameraLook3D_FirstPerson(Transform cam, Transform player, float mouseSensitivity,ref float xRotation)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -30f, 60f);

            cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            player.Rotate(Vector3.up * mouseX);
        }
    }
}


