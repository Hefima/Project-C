using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeys : MonoBehaviour
{
    public float AxisX, AxisY;

    public bool input_Mouse0;
    public bool input_Mouse1;

    public bool input_Shift;
    public bool input_Space;
    public bool input_E;

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
                    GameManager.acc.UI.ToggleUI(GameManager.acc.UI.inventoryUIObject);
                    break;
                case "P":
                    GameManager.acc.UI.ToggleUI(GameManager.acc.UI.statsUIObject);
                    break;
                case "1":
                    PlayerManager.acc.PC.AbilityI();
                    break;
                case "2":
                    PlayerManager.acc.PC.AbilityII();
                    break;
                case "3":
                    PlayerManager.acc.PC.AbilityIII();
                    break;
                case "4":
                    PlayerManager.acc.PC.Ultimate();
                    break;
            }
        }

        input_E = Input.GetKeyDown(KeyCode.E);

        input_Shift = Input.GetKey(KeyCode.LeftShift);
        input_Space = Input.GetKey(KeyCode.Space);

        input_Mouse0 = Input.GetMouseButton(0);
        input_Mouse1 = Input.GetMouseButton(1);
    }
}
