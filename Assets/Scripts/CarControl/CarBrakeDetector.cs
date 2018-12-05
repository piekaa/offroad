using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBrakeDetector : MonoBehaviour
{

    private Rigidbody2D rb;

    private float velocityInPrevFrame;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        velocityInPrevFrame = rb.velocity.magnitude;
    }

    void OnJointBreak2D(Joint2D brokenJoint)
    {
        if (brokenJoint is FixedJoint2D)
        {
            SEventSystem.FireEvent(EventNames.CAR_BRAKE);
            SEventSystem.FireEvent(EventNames.CAR_BRAKE_WITH_VELOCITY, new PMEventArgs(velocityInPrevFrame));
        }
    }
}
