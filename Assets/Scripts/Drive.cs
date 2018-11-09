using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : OrderedScript, IDrive
{

    [SerializeField]
    public float maxSpeed = 100;

    public float FrontBreakPower = 50;
    public float RearBreakPower = 50;

    private bool brakeingFront;
    private bool brakeingRear;

    private bool reverse;

    private IWheel frontWheel;

    private IWheel rearWheel;

    public float FrontRearRatio { get; set; }

    /// <summary>
    /// Accelerates the front. Invoke only in FixedUpdate
    /// </summary>
    /// <param name="power">Torque</param>
    public void Accelerate(float power)
    {
        float frontWheelRPM = Mathf.Abs(FrontWheelRPM);
        float rearWheelRPM = Mathf.Abs(RearWheelRPM);
        float frontWheelKmPerHour = Utils.WheelRpmToKmPerHour(frontWheelRPM, frontWheel.DiameterInMeters);
        float rearWheelKmPerHour = Utils.WheelRpmToKmPerHour(rearWheelRPM, rearWheel.DiameterInMeters);
        if (frontWheelKmPerHour < maxSpeed && rearWheelKmPerHour < maxSpeed)
        {
            var sign = reverse ? 1 : -1;
            frontWheel.AddTorque(power * FrontRearRatio * sign);
            rearWheel.AddTorque(power * (1 - FrontRearRatio) * sign);
        }
    }

    public void Brake(float power)
    {
        //todo add FrontRearBrakeRatio
        BrakeFront(power);
        BrakeRear(power);
    }

    private void BrakeFront(float power)
    {
        if (power > 0)
        {
            brakeingFront = true;
        }
        doBrake(frontWheel, power * FrontBreakPower);
    }

    private void BrakeRear(float power)
    {
        if (power > 0)
        {
            brakeingRear = true;
        }
        doBrake(rearWheel, power * RearBreakPower);
    }

    private void doBrake(IWheel wheel, float power)
    {
        wheel.AngularDrag = power;

        var motor = wheel.Joint.motor;
        float sign = Mathf.Sign(wheel.AngularVelocity);
        float newSpeed = Mathf.Abs(wheel.AngularVelocity) - power;

        newSpeed = Mathf.Max(newSpeed, 0);

        motor.motorSpeed = newSpeed * sign;

        wheel.Joint.useMotor = true;
        wheel.Joint.motor = motor;

    }

    public float FrontWheelRPM { get { return frontWheel.AngularVelocity / 6; } }

    public float RearWheelRPM { get { return rearWheel.AngularVelocity / 6; } }


    public override void OrderedFixedUpdate()
    {
        if (!brakeingFront)
        {
            frontWheel.Joint.useMotor = false;
            frontWheel.AngularDrag = 0;
        }

        if (!brakeingRear)
        {
            rearWheel.Joint.useMotor = false;
            rearWheel.AngularDrag = 0;
        }
        brakeingFront = false;
        brakeingRear = false;
    }

    public bool ToggleReverse()
    {
        float frontWheelRPM = Mathf.Abs(FrontWheelRPM);
        float rearWheelRPM = Mathf.Abs(RearWheelRPM);
        float frontWheelKmPerHour = Utils.WheelRpmToKmPerHour(frontWheelRPM, frontWheel.DiameterInMeters);
        float rearWheelKmPerHour = Utils.WheelRpmToKmPerHour(rearWheelRPM, rearWheel.DiameterInMeters);
        if (frontWheelKmPerHour < 5 && rearWheelKmPerHour < 5)
        {
            reverse = !reverse;
        }
        return reverse;
    }

    public void SetFrontWheel(IWheel wheel)
    {
        frontWheel = wheel;
    }

    public void SetRearWheel(IWheel wheel)
    {
        rearWheel = wheel;
    }
}
