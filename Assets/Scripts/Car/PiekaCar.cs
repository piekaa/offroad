using System.Collections;
using System.Collections.Generic;
using Pieka.Utils;
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

        [SerializeField]
        private SpriteRenderer shockAbsorberPrefab;

        [SerializeField]
        private GameObject frontPart;

        [SerializeField]
        private GameObject rearPart;

        [SerializeField]
        private GameObject middlePart;

        private Rigidbody2D middlePartRigidbody;

        private WheelJoint2D frontWheelJoint;

        private WheelJoint2D rearWheelJoint;

        private SpriteRenderer frontWheelShockAbsorber;

        private SpriteRenderer rearWheelShockAbsorber;

        private float shockAbsorberHeight;

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

            frontWheelShockAbsorber = Instantiate(shockAbsorberPrefab, frontWheel.transform.position, Quaternion.identity);

            rearWheelShockAbsorber = Instantiate(shockAbsorberPrefab, rearWheel.transform.position, Quaternion.identity);

            var shockAbsorberPositions = SpriteUtils.GetWolrdPositions(shockAbsorberPrefab);
            shockAbsorberHeight = Vector2.Distance(shockAbsorberPositions.TopLeft, shockAbsorberPositions.BottomLeft);

            Drive.SetJoints(frontWheelJoint, rearWheelJoint);

            middlePartRigidbody = middlePart.GetComponent<Rigidbody2D>();
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

        void Update()
        {
            updateShockAbsorber(frontWheelShockAbsorber, frontPart, frontWheel);
            updateShockAbsorber(rearWheelShockAbsorber, rearPart, rearWheel);
        }

        private void updateShockAbsorber(SpriteRenderer shockAbsorber, GameObject carPart, Wheel wheel)
        {
            var wheelCenter = SpriteUtils.GetWolrdPositions(wheel.GetComponent<SpriteRenderer>()).Center;
            var frontPartCenter = SpriteUtils.GetWolrdPositions(carPart.GetComponent<SpriteRenderer>()).Center;
            var distance = Vector2.Distance(wheelCenter, frontPartCenter);

            shockAbsorber.transform.localScale = new Vector3(shockAbsorberPrefab.transform.localScale.x, shockAbsorberPrefab.transform.localScale.y * (distance / shockAbsorberHeight));
            shockAbsorber.transform.rotation = carPart.transform.rotation;
            shockAbsorber.transform.position = wheel.transform.position;
        }

        public Vector3 GetVelocity()
        {
            return middlePartRigidbody.velocity;
        }
    }
}