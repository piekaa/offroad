using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{
    private Dictionary<Wheel, List<float>> wheelVelocitySignHistory = new Dictionary<Wheel, List<float>>();

    public float fullStopThreshold = 70;

    public float maxSpeed = 100;

    public float BreakPower = 3;

    private bool brakeingFront;
    private bool brakeingRear;

    private bool reverse;

    private Wheel frontWheel;

    private Wheel rearWheel;

    public float FrontRearDriveRatio { get; set; }

    public float FrontRearBrakeRatio { get; set; }

    private WheelJoint2D frontJoint;

    private WheelJoint2D rearJoint;

    private float brakeThrottle;

    public float FrontWheelKmPerH { get; private set; }

    public float RearWheelKmPerH { get; private set; }

    public void Accelerate(float power)
    {
        var sign = reverse ? 1 : -1;
        if (FrontWheelKmPerH < maxSpeed)
        {
            frontWheel.AddTorque(power * FrontRearDriveRatio * sign);
        }
        if (RearWheelKmPerH < maxSpeed)
        {
            rearWheel.AddTorque(power * (1 - FrontRearDriveRatio) * sign);
        }
    }

    public void Brake(float throttle)
    {
        brakeThrottle = throttle;
    }

    void Update()
    {
        float frontWheelRPM = Mathf.Abs(FrontWheelRpm);
        float rearWheelRPM = Mathf.Abs(RearWheelRpm);
        FrontWheelKmPerH = CalculateUtils.WheelRpmToKmPerHour(frontWheelRPM, frontWheel.DiameterInMeters);
        RearWheelKmPerH = CalculateUtils.WheelRpmToKmPerHour(rearWheelRPM, rearWheel.DiameterInMeters);
    }

    void FixedUpdate()
    {
        doBrake(frontWheel, frontJoint, brakeThrottle * BreakPower * FrontRearBrakeRatio);
        doBrake(rearWheel, rearJoint, brakeThrottle * BreakPower * (1 - FrontRearBrakeRatio));
    }

    private void doBrake(Wheel wheel, WheelJoint2D joint, float power)
    {
        var angularVelocity = wheel.AngularVelocity;
        wheelVelocitySignHistory[wheel].Add(Mathf.Sign(angularVelocity));

        if (power * fullStopThreshold > Mathf.Abs(angularVelocity))
        {
            wheel.AngularVelocity = 0;
            joint.useMotor = true;
        }
        else
        {
            var sign0 = wheelVelocitySignHistory[wheel][0];
            var sign1 = wheelVelocitySignHistory[wheel][1];
            var sign2 = wheelVelocitySignHistory[wheel][2];

            if (sign0 * sign1 == -1 && sign1 * sign2 == -1)
            {
                wheel.AngularVelocity = 0;
                joint.useMotor = true;
            }
            else
            {
                var sign = Mathf.Sign(angularVelocity);
                wheel.AddTorque(power * -sign);
                joint.useMotor = false;
            }
        }
        if (power == 0)
        {
            wheelVelocitySignHistory[wheel].Clear();
            wheelVelocitySignHistory[wheel].Add(0);
            wheelVelocitySignHistory[wheel].Add(0);
        }

        while (wheelVelocitySignHistory[wheel].Count > 3)
        {
            wheelVelocitySignHistory[wheel].RemoveAt(0);
        }
    }

    public float FrontWheelRpm { get { return frontWheel.AngularVelocity / 6; } }

    public float RearWheelRpm { get { return rearWheel.AngularVelocity / 6; } }

    public bool ToggleReverse()
    {
        float frontWheelRPM = Mathf.Abs(FrontWheelRpm);
        float rearWheelRPM = Mathf.Abs(RearWheelRpm);
        float frontWheelKmPerHour = CalculateUtils.WheelRpmToKmPerHour(frontWheelRPM, frontWheel.DiameterInMeters);
        float rearWheelKmPerHour = CalculateUtils.WheelRpmToKmPerHour(rearWheelRPM, rearWheel.DiameterInMeters);
        if (frontWheelKmPerHour < 5 && rearWheelKmPerHour < 5)
        {
            reverse = !reverse;
        }
        return reverse;
    }

    public void SetFrontWheel(Wheel wheel)
    {
        frontWheel = wheel;
        wheelVelocitySignHistory.Add(wheel, new List<float>());
        wheelVelocitySignHistory[wheel].Add(0);
        wheelVelocitySignHistory[wheel].Add(0);
    }

    public void SetRearWheel(Wheel wheel)
    {
        rearWheel = wheel;
        wheelVelocitySignHistory.Add(wheel, new List<float>());
        wheelVelocitySignHistory[wheel].Add(0);
        wheelVelocitySignHistory[wheel].Add(0);
    }


    private void setMotorSpeed(WheelJoint2D joint, float speed)
    {
        var motor = joint.motor;
        motor.motorSpeed = speed;
        joint.motor = motor;
    }

    private void setUseMotor(WheelJoint2D joint, bool useMotor)
    {
        joint.useMotor = useMotor;
    }

    public void SetJoints(WheelJoint2D front, WheelJoint2D rear)
    {
        frontJoint = front;
        rearJoint = rear;
    }
}
