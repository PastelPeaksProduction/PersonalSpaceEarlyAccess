﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject Camera;
    public GameObject ControlPos;
    public GameObject OrigMainPos;
    public GameObject BackMainPos;
    public GameObject AwayMenuPos;
    public GameObject MainMenuParent;
    public GameObject InstrucParent;
    public GameObject InstructionPos;
    public GameObject InstructionPosExit;

    private bool ControlTrans = false;
    private bool BackTrans = false;
    private bool HideMainTrans = false;
    private bool ShowMainTrans;
    private bool credittrans = false;

    private Vector3 prevPos;
    private CameraAnimation cam;

    void Awake()
    {
        cam = Camera.GetComponent<CameraAnimation>();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("2.5Cutscene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMain()
    {
        ShowMainTrans = true;
        HideMainTrans = false;
        credittrans = true;
        // BackTrans = true;
    }

    public void ShowControls()
    {
        prevPos = InstrucParent.transform.position;
        HideMainTrans = true;
        ControlTrans = true;
    }

    public void ControlsClicked()
    {
        HideMainTrans = true;
        credittrans = false;
        cam.GOTOCredits();
    }
    public void LevelsClicked()
    {
        HideMainTrans = true;
        cam.GOTOLevels();
    }

    public void LevelsBack()
    {
        HideMainTrans = false;
        ShowMainTrans = true;
        credittrans = false;
        cam.LevelsBackMain();
    }

    public void ControlBack()
    {
        HideMainTrans = false;
        ShowMainTrans = true;
        credittrans = false;
        cam.ControlsBackMain();
    }



    private void FixedUpdate()
    {
        if (HideMainTrans)
        {
            MainMenuParent.GetComponent<RectTransform>().rotation = Quaternion.RotateTowards(MainMenuParent.GetComponent<RectTransform>().rotation, AwayMenuPos.GetComponent<RectTransform>().rotation, 50 * Time.deltaTime);
        }
        if (ShowMainTrans && credittrans)
        {
            HideMainTrans = false;
            MoveCam(OrigMainPos, ref ShowMainTrans);
            InstrucParent.transform.position = Vector3.MoveTowards(InstrucParent.transform.position, prevPos, 30 * Time.deltaTime);


            MainMenuParent.GetComponent<RectTransform>().rotation = Quaternion.RotateTowards(MainMenuParent.GetComponent<RectTransform>().rotation, BackMainPos.GetComponent<RectTransform>().rotation, 30 * Time.deltaTime);

        }
        else if (ShowMainTrans)
        {
            MainMenuParent.GetComponent<RectTransform>().rotation = Quaternion.RotateTowards(MainMenuParent.GetComponent<RectTransform>().rotation, BackMainPos.GetComponent<RectTransform>().rotation, 30 * Time.deltaTime);
        }

        if (ControlTrans)
        {
            MoveCam(ControlPos, ref ControlTrans);
            InstrucParent.transform.position = Vector3.MoveTowards(InstrucParent.transform.position, InstructionPos.transform.position, 20 * Time.deltaTime);

        }
        else if (BackTrans)
        {
            MoveCam(BackMainPos, ref BackTrans);

        }
    }

    private void MoveCam(GameObject moveTo, ref bool transBool)
    {
        if (Camera.transform.position == moveTo.transform.position && Camera.transform.rotation == moveTo.transform.rotation)
            transBool = false;

        Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, moveTo.transform.position, 50 * Time.deltaTime);
        Camera.transform.rotation = Quaternion.RotateTowards(Camera.transform.rotation, moveTo.transform.rotation, 90 * Time.deltaTime);
    }

    private void EnsureInstr()
    {

    }
}
