using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pieka.Car
{
    class Drive : MonoBehaviour, IDrive
    {
        private Dictionary<IWheel, List<float>> wheelVelocitySignHistory = new Dictionary<IWheel, List<float>>();

        public float fullStopThreshold = 70;

        public float maxSpeed = 100;

        public float BreakPower = 3;

        private bool brakeingFront;
        private bool brakeingRear;

        private bool reverse;

        private IWheel frontWheel;

        private IWheel rearWheel;

        public float FrontRearDriveRatio { get; set; }

        public float FrontRearBrakeRatio { get; set; }

        private WheelJoint2D frontJoint;

        private WheelJoint2D rearJoint;

        private float brakeThrottle;

        public void Accelerate(float power)
        {
            float frontWheelRPM = Mathf.Abs(FrontWheelRpm);
            float rearWheelRPM = Mathf.Abs(RearWheelRpm);
            float frontWheelKmPerHour = Utils.WheelRpmToKmPerHour(frontWheelRPM, frontWheel.DiameterInMeters);
            float rearWheelKmPerHour = Utils.WheelRpmToKmPerHour(rearWheelRPM, rearWheel.DiameterInMeters);
            var sign = reverse ? 1 : -1;
            if (frontWheelKmPerHour < maxSpeed)
            {
                frontWheel.AddTorque(power * FrontRearDriveRatio * sign);
            }
            if (rearWheelKmPerHour < maxSpeed)
            {
                rearWheel.AddTorque(power * (1 - FrontRearDriveRatio) * sign);
            }
        }

        public void Brake(float throttle)
        {
            brakeThrottle = throttle;
        }

        void FixedUpdate()
        {
            doBrake(rearWheel, rearJoint, brakeThrottle * BreakPower * (1 - FrontRearBrakeRatio));
            doBrake(frontWheel, frontJoint, brakeThrottle * BreakPower * FrontRearBrakeRatio);
        }

        private void doBrake(IWheel wheel, WheelJoint2D joint, float power)
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
            wheelVelocitySignHistory.Add(wheel, new List<float>());
            wheelVelocitySignHistory[wheel].Add(0);
            wheelVelocitySignHistory[wheel].Add(0);
        }

        public void SetRearWheel(IWheel wheel)
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
}