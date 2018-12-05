using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBrakeDetector : MonoBehaviour
{

    void OnJointBreak2D(Joint2D brokenJoint)
    {
        if (brokenJoint is FixedJoint2D)
        {
            SEventSystem.FireEvent(EventNames.CAR_BRAKE);
        }
    }
}
