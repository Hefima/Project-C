using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeys : MonoBehaviour
{
    public float AxisX, AxisY;

    void Update()
    {
        AxisX = Input.GetAxisRaw("Horizontal");
        AxisY = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
        }

        if (Input.anyKeyDown)
        {
            switch (Input.inputString.ToUpper())
            {
                case "I":
                    
                    break;
            }
        }
    }
}
