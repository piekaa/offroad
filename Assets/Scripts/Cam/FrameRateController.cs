using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pieka.Cam
{
    public class FrameRateController : MonoBehaviour
    {
        void Start()
        {
            Application.targetFrameRate = 60;
        }
    }
}