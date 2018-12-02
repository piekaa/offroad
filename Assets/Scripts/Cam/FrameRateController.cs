using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateController : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
    }
}
