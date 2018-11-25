using System.Collections;
using System.Collections.Generic;
using Pieka.Effects;
using Pieka.Utils;
using UnityEngine;

namespace Pieka.Car
{
    public class PiekaCar : MonoBehaviour, ICar
    {

        private const float BURN_SPEED_RANGE = 25;

        private float ACCEPTABLE_SPEED_DIFFERENCE = 10;

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

        private Collider2D[] colliders;

        private Collider2D frontWheelCollider;
        private Collider2D rearWheelCollider;

        private OnBurn onBurn;

        private float velocityInKmPerH;

        private ContactPoint2D[] burnContacts = new ContactPoint2D[10];

        private SpriteRenderer frontWheelSpriteRenderer;
        private SpriteRenderer rearWheelSpriteRenderer;

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

            colliders = GetComponentsInChildren<Collider2D>();

            frontWheelCollider = frontWheel.GetComponent<Collider2D>();
            rearWheelCollider = rearWheel.GetComponent<Collider2D>();

            frontWheelSpriteRenderer = frontWheel.GetComponent<SpriteRenderer>();
            rearWheelSpriteRenderer = rearWheel.GetComponent<SpriteRenderer>();
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
            velocityInKmPerH = CalculateUtils.UnitsPerSecondToKmPerH(middlePartRigidbody.velocity.magnitude);
            updateShockAbsorber(frontWheelShockAbsorber, frontPart, frontWheel);
            updateShockAbsorber(rearWheelShockAbsorber, rearPart, rearWheel);
            handleBurn();
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

        private void handleBurn()
        {
            handleBurn(frontWheelCollider, frontWheelSpriteRenderer, BurnInfo.WhichWheelEnum.FRONT, frontWheel.AngularVelocity < 0 ? BurnInfo.DirectionEnum.LEFT : BurnInfo.DirectionEnum.RIGHT, Drive.FrontWheelKmPerH);
            handleBurn(rearWheelCollider, rearWheelSpriteRenderer, BurnInfo.WhichWheelEnum.REAR, rearWheel.AngularVelocity < 0 ? BurnInfo.DirectionEnum.LEFT : BurnInfo.DirectionEnum.RIGHT, Drive.RearWheelKmPerH);
        }

        private void handleBurn(Collider2D wheelCollider, SpriteRenderer spriteRenderer, BurnInfo.WhichWheelEnum whichWheel, BurnInfo.DirectionEnum direction, float wheelKmPerH)
        {
            if (wheelCollider.IsTouchingLayers(Consts.FloorLayerMask))
            {
                var carSpeedPlusAcceptableDifference = velocityInKmPerH + ACCEPTABLE_SPEED_DIFFERENCE;
                var wheelSpeed = wheelKmPerH;
                if (wheelSpeed > carSpeedPlusAcceptableDifference)
                {
                    if (onBurn != null)
                    {
                        var filter = new ContactFilter2D();
                        filter.layerMask = Consts.FloorLayerMask;
                        var numberOfContacts = wheelCollider.GetContacts(filter, burnContacts);
                        for (int i = 0; i < numberOfContacts; i++)
                        {
                            var point = burnContacts[i].point;
                            var power = Mathf.Clamp((wheelSpeed - carSpeedPlusAcceptableDifference) / BURN_SPEED_RANGE, 0, 1);
                            var burnInfo = new BurnInfo(whichWheel, direction, point, power);
                            onBurn(burnInfo);
                        }
                    }
                }
            }
        }

        public Sparkable[] GetSparkables()
        {
            return GetComponentsInChildren<Sparkable>();
        }

        public bool IsInAir()
        {
            foreach (var collider in colliders)
            {
                if (collider.IsTouchingLayers())
                {
                    return false;
                }
            }
            return true;
        }

        public float GetAngle()
        {
            var angle = middlePart.transform.rotation.eulerAngles.z;
            while (angle < 0)
            {
                angle += 360;
            }
            while (angle >= 360)
            {
                angle -= 360;
            }
            return angle;
        }

        public int WheelsOnFloorCount()
        {
            var result = 0;
            result += frontWheelCollider.IsTouchingLayers() ? 1 : 0;
            result += rearWheelCollider.IsTouchingLayers() ? 1 : 0;
            return result;
        }

        public void RegisterOnBurn(OnBurn onBurn)
        {
            this.onBurn += onBurn;
        }

        public void UnregisterOnBurn(OnBurn onBurn)
        {
            this.onBurn -= onBurn;
        }
    }
}