using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    public Drive Drive { get; private set; }

    private Engine Engine { get; set; }

    public Wheel FrontWheel;

    public Wheel RearWheel;

    [SerializeField]
    private GameObject frontPart;

    public Rigidbody2D middlePartRigidbody;

    private WheelJoint2D frontWheelJoint;

    private WheelJoint2D rearWheelJoint;

    private Collider2D[] colliders;

    public Collider2D FrontWheelCollider { get; private set; }
    public Collider2D RearWheelCollider { get; private set; }

    public SpriteRenderer FrontWheelSpriteRenderer { get; private set; }
    public SpriteRenderer RearWheelSpriteRenderer { get; private set; }

    public CarStateDetector burnDetector;

    void Awake()
    {
        Drive = GetComponentInChildren<Drive>();
        Engine = GetComponentInChildren<Engine>();
        Drive.SetFrontWheel(FrontWheel);
        Drive.SetRearWheel(RearWheel);

        var joints = GetComponentsInChildren<WheelJoint2D>();
        if (joints.Length != 2)
        {
            Debug.Log("Car has wrong number of WheelJoints2D: " + joints.Length);
        }
        foreach (var joint in joints)
        {
            if (joint.connectedBody == FrontWheel.GetComponent<Rigidbody2D>())
            {
                frontWheelJoint = joint;
            }
            if (joint.connectedBody == RearWheel.GetComponent<Rigidbody2D>())
            {
                rearWheelJoint = joint;
            }
        }

        Drive.SetJoints(frontWheelJoint, rearWheelJoint);

        middlePartRigidbody = frontPart.GetComponent<Rigidbody2D>();

        colliders = GetComponentsInChildren<Collider2D>();

        FrontWheelCollider = FrontWheel.GetComponent<Collider2D>();
        RearWheelCollider = RearWheel.GetComponent<Collider2D>();

        FrontWheelSpriteRenderer = FrontWheel.GetComponent<SpriteRenderer>();
        RearWheelSpriteRenderer = RearWheel.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        burnDetector.StartDetection(this);
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

    public void SetFrontRearDriveRatio(float ratio)
    {
        Drive.FrontRearDriveRatio = ratio;
    }

    public void SetFrontRearBrakeRatio(float ratio)
    {
        Drive.FrontRearBrakeRatio = ratio;
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
        var angle = frontPart.transform.rotation.eulerAngles.z;
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
        result += FrontWheelCollider.IsTouchingLayers() ? 1 : 0;
        result += RearWheelCollider.IsTouchingLayers() ? 1 : 0;
        return result;
    }

    public GameObject[] GetBrakeables()
    {
        return new GameObject[] { frontPart };
    }
}