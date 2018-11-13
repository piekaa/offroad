using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pieka.Car
{
    public class PiekaCar : MonoBehaviour, ICar
    {
        private IDrive Drive { get; set; }

        private IEngine Engine { get; set; }

        [SerializeField]
        private Wheel frontWheel;

        [SerializeField]
        private Wheel rearWheel;

        private WheelJoint2D frontWheelJoint;

        private WheelJoint2D rearWheelJoint;

        void Awake()
        {
            Drive = GetComponentInChildren<Drive>();
            Engine = GetComponentInChildren<Engine>();
            Drive.SetFrontWheel(frontWheel);
            Drive.SetRearWheel(rearWheel);

            var joints = GetComponentsInChildren<WheelJoint2D>();
            if (joints.Length != 2)
            {
                Debug.Log("Car has wrong number of WheelJoints2D: " + joints.Length);
            }
            foreach (var joint in joints)
            {
                if (joint.connectedBody == frontWheel.GetComponent<Rigidbody2D>())
                {
                    frontWheelJoint = joint;
                }
                if (joint.connectedBody == rearWheel.GetComponent<Rigidbody2D>())
                {
                    rearWheelJoint = joint;
                }
            }

            Drive.SetJoints(frontWheelJoint, rearWheelJoint);

        }

        public void SetFrontSuspensionFrequency(float frequency)
        {
            var suspension = frontWheelJoint.suspension;
            suspension.frequency = frequency;
            frontWheelJoint.suspension = suspension;
        }

        public void SetFrontDampingRatio(float dampingRatio)
        {
            var suspension = frontWheelJoint.suspension;
            suspension.dampingRatio = dampingRatio;
            frontWheelJoint.suspension = suspension;
        }

        public void SetRearSuspensionFrequency(float frequency)
        {
            var suspension = rearWheelJoint.suspension;
            suspension.frequency = frequency;
            rearWheelJoint.suspension = suspension;
        }

        public void SetRearDampingRatio(float dampingRatio)
        {
            var suspension = rearWheelJoint.suspension;
            suspension.dampingRatio = dampingRatio;
            rearWheelJoint.suspension = suspension;
        }

        public void SetFrontSuspensionHeight(float height)
        {
            frontWheelJoint.anchor = new Vector2(frontWheelJoint.anchor.x, -height);
        }

        public void SetRearSuspensionHeight(float height)
        {
            rearWheelJoint.anchor = new Vector2(rearWheelJoint.anchor.x, -height);
        }

        public void Brake(float throttle)
        {
            Drive.Brake(throttle);
        }

        public void Accelerate(float throttle)
        {
            Engine.Throttle = throttle;
        }

        public bool ToggleReverse()
        {
            return Drive.ToggleReverse();
        }

        public CarInfo GetCarInfo()
        {
            return new CarInfo(frontWheel.DiameterInMeters, rearWheel.DiameterInMeters, Drive.FrontWheelRpm, Drive.RearWheelRpm);
        }

        public void SetFrontRearDriveRatio(float ratio)
        {
            Drive.FrontRearDriveRatio = ratio;
        }

        public void SetFrontRearBrakeRatio(float ratio)
        {
            Drive.FrontRearBrakeRatio = ratio;
        }
    }
}