using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pieka.Car
{
    class Drive : OrderedScript, IDrive
    {
        private float FullStopThreshold = 70;

        [SerializeField]
        public float maxSpeed = 100;

        public float BreakPower = 3;

        private bool brakeingFront;
        private bool brakeingRear;

        private bool reverse;

        private IWheel frontWheel;

        private IWheel rearWheel;

        public float FrontRearRatio { get; set; }

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
                frontWheel.AddTorque(power * FrontRearRatio * sign);
            }
            if (rearWheelKmPerHour < maxSpeed)
            {
                rearWheel.AddTorque(power * (1 - FrontRearRatio) * sign);
            }
        }

        public void Brake(float throttle)
        {
            brakeThrottle = throttle;
        }

        void FixedUpdate()
        {
            //todo add FrontRearBrakeRatio
            doBrake(frontWheel, frontJoint, brakeThrottle * BreakPower);
            doBrake(rearWheel, rearJoint, brakeThrottle * BreakPower);
        }

        private void doBrake(IWheel wheel, WheelJoint2D joint, float power)
        {
            if (power * FullStopThreshold > Mathf.Abs(wheel.AngularVelocity))
            {
                wheel.AngularVelocity = 0;
                joint.useMotor = true;
            }
            else
            {
                var sign = Mathf.Sign(wheel.AngularVelocity);
                wheel.AddTorque(power * -sign);
                joint.useMotor = false;
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
        }

        public void SetRearWheel(IWheel wheel)
        {
            rearWheel = wheel;
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