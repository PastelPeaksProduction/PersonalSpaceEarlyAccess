﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SiliconDroid;

public class OnScreenJoystickController : MonoBehaviour
{
    public bool testing = false;
    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android || testing)
        {
            const float K_F_SIZE = 0.125f;
            SD_Joystick.fnc_Create_Start();
            SD_Joystick.fnc_SetColor(Color.magenta);
            SD_Joystick.fnc_Create_2DStick(SD_Joystick.ANCHOR.TOP_LEFT, K_F_SIZE, K_F_SIZE, K_F_SIZE);

            SD_Joystick.fnc_Create_1DStick(SD_Joystick.ANCHOR.TOP_RIGHT, K_F_SIZE, K_F_SIZE, 1.5f * K_F_SIZE, K_F_SIZE);
            SD_Joystick.fnc_2DStick_SetVisible(0, true);
            SD_Joystick.fnc_1DStick_SetVisible(0, true);
        }
        
    }

    public Vector3 Movement()
    {
        if (Application.platform == RuntimePlatform.Android || testing)
        {
            Vector2 vJoy = SD_Joystick.fnc_2DStick_GetValue(0);
            Vector3 input = new Vector3(vJoy.x, 0f, vJoy.y);
            return input;
        }
        else
        {
            return Vector3.zero;
        }
            
    }

    public float Rotation()
    {
        if (Application.platform == RuntimePlatform.Android || testing)
        {
            float vJoy = SD_Joystick.fnc_1DStick_GetValue(0);
            return vJoy;
        }
        else
        {
            return 0f;
        }
            

    }
}
