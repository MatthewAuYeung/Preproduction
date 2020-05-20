﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Invector.CharacterController;

public class DebugRestart : MonoBehaviour
{
    public bool usingDebug;
    public bool usingMouse = false;

    private vThirdPersonInput input;
    private void Awake()
    {
        input = GetComponentInChildren<vThirdPersonInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if(usingDebug)
        {
            if (Input.GetButtonDown("DebugRestart"))
            {
                Restart();
            }
        }

        if (usingMouse)
        {
            input.rotateCameraXInput = "Mouse X";
            input.rotateCameraYInput = "Mouse Y";
        }
    }

    public static void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
