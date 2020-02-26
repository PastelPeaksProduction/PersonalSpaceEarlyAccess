﻿///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//   ____ ___ _     ___ ____ ___  _   _   ____  ____   ___ ___ ____  
//  / ___|_ _| |   |_ _/ ___/ _ \| \ | | |  _ \|  _ \ / _ \_ _|  _ \ 
//  \___ \| || |    | | |  | | | |  \| | | | | | |_) | | | | || | | |
//   ___) | || |___ | | |__| |_| | |\  | | |_| |  _ <| |_| | || |_| |
//  |____/___|_____|___\____\___/|_| \_| |____/|_| \_\\___/___|____/ 
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SiliconDroid
{
    //#############################################################################################################################
    //	CLASS: SD_Joystick_control_2dstick
    //#############################################################################################################################
    public class SD_Joystick_control_2dstick : SD_Joystick_control_baseclass
    {

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	CONSTANTS

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	WORKING DATA
        private class WORKING_DATA
        {
            public Vector2 vValue = Vector2.zero;
        }
        WORKING_DATA v = new WORKING_DATA();

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	 ____  _   _ ____  _     ___ ____ 
        //	|  _ \| | | | __ )| |   |_ _/ ___|
        //	| |_) | | | |  _ \| |    | | |    
        //	|  __/| |_| | |_) | |___ | | |___ 
        //	|_|    \___/|____/|_____|___\____|
        //
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	CREATE
        public bool fnc_Create(Bounds cBounds_scr, int iCtrlSprite, int iBgndSprite)
        {
            //  INJECT GROUP OBJ
            b.tParentGroup = new GameObject(this.name).transform;
            SD_Joystick_factory.fnc_ChildToTransform(b.tParentGroup, this.transform.parent);

            //  ACTIVE CONTROL
            Bounds cBounds_ctl = SD_Joystick_factory.fnc_Bounds_ScreenToWorld(cBounds_scr, 0, b.tParentGroup);
            float fDiameter = (cBounds_ctl.size.x + cBounds_ctl.size.y) * 0.5f;

            Transform tPad = SD_Joystick_factory.fnc_CreateMesh(this.gameObject, fDiameter, fDiameter, iCtrlSprite);
            SD_Joystick_factory.fnc_ChildToTransform(tPad, b.tParentGroup);
            this.transform.Translate(cBounds_ctl.center.x, cBounds_ctl.center.y, 0, Space.Self);
            tPad.name = SD_Joystick_root.K_S_NAME_CONTROL;

            SD_Joystick_factory.fnc_SetAsCollider(tPad);
            b.tControlToMove = tPad;
            b.fMoveLimit = fDiameter * 0.333f;

            //  DECOR
            cBounds_ctl = SD_Joystick_factory.fnc_Bounds_ScreenToWorld(cBounds_scr, 1, b.tParentGroup);
            fDiameter *= 1.25f;

            b.tControlDecor = SD_Joystick_factory.fnc_CreateMesh(null, fDiameter, fDiameter, iBgndSprite);
            SD_Joystick_factory.fnc_ChildToTransform(b.tControlDecor, b.tParentGroup, true);
            b.tControlDecor.Translate(cBounds_ctl.center.x, cBounds_ctl.center.y, 0, Space.Self);
            b.tControlDecor.name = SD_Joystick_root.K_S_NAME_DECORATION;

            //  DONE
            fnc_Touch_Start(Vector3.zero);
            return true;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	GET VALUE
        public Vector2 GetValue()
        {
            v.vValue = (this.transform.localPosition - b.vOriginalPos) / b.fMoveLimit;
            return v.vValue;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	 ___ _   _ _   _ _____ ____  ___ _____ _____ ____  
        //	|_ _| \ | | | | | ____|  _ \|_ _|_   _| ____|  _ \ 
        //	 | ||  \| | |_| |  _| | |_) || |  | | |  _| | | | |
        //	 | || |\  |  _  | |___|  _ < | |  | | | |___| |_| |
        //	|___|_| \_|_| |_|_____|_| \_\___| |_| |_____|____/ 
        //
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	CLOCK PER FRAME OPTION
        public override void fnc_Clock()
        {
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	TOUCH MOVE
        Vector3 vActivePos;
        public override void fnc_Touch_Move(Vector3 vPos)
        {
            if (!b.bVisible)
            {
                return;
            }
            if (b.tControlToMove)
            {
                vActivePos = vPos - b.vTouchDown;
                vActivePos.z = 0;
                if(vActivePos.magnitude > b.fMoveLimit)
                {
                    vActivePos.Normalize();
                    vActivePos *= b.fMoveLimit;
                }
                b.tControlToMove.localPosition = b.vOriginalPos + vActivePos;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //	 ____  ____  _____     ___  _____ _____ 
        //	|  _ \|  _ \|_ _\ \   / / \|_   _| ____|
        //	| |_) | |_) || | \ \ / / _ \ | | |  _|  
        //	|  __/|  _ < | |  \ V / ___ \| | | |___ 
        //	|_|   |_| \_\___|  \_/_/   \_\_| |_____|
        //
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    }
}