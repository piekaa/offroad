using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour, ICar
{
    public IDrive Drive { get; set; }

    public IEngine Engine { get; set; }

    [SerializeField]
    private Wheel frontWheel;

    [SerializeField]
    private Wheel rearWheel;

    [SerializeField]
    private WheelJoint2D FrontWheelJoint;

    [SerializeField]
    private WheelJoint2D RearWheelJoint;

    void Awake()
    {
        Drive = GetComponentInChildren<Drive>();
        Engine = GetComponentInChildren<Engine>();
        Drive.SetFrontWheel(frontWheel);
        Drive.SetRearWheel(rearWheel);
    }

    public void SetFrontSuspensionFrequency(float frequency)
    {
        var suspension = FrontWheelJoint.suspension;
        suspension.frequency = frequency;
        FrontWheelJoint.suspension = suspension;
    }

    public void SetFrontDampingRatio(float dampingRatio)
    {
        var suspension = FrontWheelJoint.suspension;
        suspension.frequency = dampingRatio;
        FrontWheelJoint.suspension = suspension;
    }

    public void SetRearSuspensionFrequency(float frequency)
    {
        var suspension = RearWheelJoint.suspension;
        suspension.frequency = frequency;
        RearWheelJoint.suspension = suspension;
    }

    public void SetRearDampingRatio(float dampingRatio)
    {
        var suspension = RearWheelJoint.suspension;
        suspension.frequency = dampingRatio;
        RearWheelJoint.suspension = suspension;
    }

    public IWheel FrontWheel
    {
        get
        {
            return frontWheel;
        }

    }
    public IWheel RearWheel
    {
        get
        {
            return rearWheel;
        }
    }
}
