﻿using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class cameraClick : MonoBehaviour // permet de faire en sorte que la camera ne bouge que si le joueur appuie sur le clique droit de sa souris
{
    private bool _freeLookActive;

    // Use this for initialization
    private void Start()
    {
        CinemachineCore.GetInputAxis = GetInputAxis;
    }

    private void Update()
    {
        _freeLookActive = Input.GetMouseButton(1); // 0 = left mouse btn or 1 = right
    }

    private float GetInputAxis(string axisName)
    {
        return !_freeLookActive ? 0 : Input.GetAxis(axisName == "Mouse Y" ? "Mouse Y" : "Mouse X");
    }
}

