﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    void LateUpdate()
    {
        Camera.main.transform.rotation = Quaternion.identity;
    }
}
