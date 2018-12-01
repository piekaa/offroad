using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour, ICar
{

    public IDrive Drive { get; private set; }
    private IEngine Engine { get; set; }

    public Wheel FrontWheel;

    public Wheel RearWheel;

    [SerializeField]
    private SpriteRenderer shockAbsorberPrefab;

    [SerializeField]
    private GameObject frontPart;

    [SerializeField]
    private GameObject rearPart;

    [SerializeField]
    private GameObject middlePart;

    public Rigidbody2D middlePartRigidbody;

    private WheelJoint2D frontWheelJoint;

    private WheelJoint2D rearWheelJoint;

    private SpriteRenderer frontWheelShockAbsorber;

    private SpriteRenderer rearWheelShockAbsorber;

    private float shockAbsorberHeight;

    private Collider2D[] colliders;

    public Collider2D FrontWheelCollider { get; private set; }
    public Collider2D RearWheelCollider { get; private set; }

    private OnBurn onBurn;

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

        frontWheelShockAbsorber = Instantiate(shockAbsorberPrefab, FrontWheel.transform.position, Quaternion.identity);

        rearWheelShockAbsorber = Instantiate(shockAbsorberPrefab, RearWheel.transform.position, Quaternion.identity);

        var shockAbsorberPositions = SpriteUtils.GetWolrdPositions(shockAbsorberPrefab);
        shockAbsorberHeight = Vector2.Distance(shockAbsorberPositions.TopLeft, shockAbsorberPositions.BottomLeft);

        Drive.SetJoints(frontWheelJoint, rearWheelJoint);

        middlePartRigidbody = middlePart.GetComponent<Rigidbody2D>();

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

    public bool ToggleReverse()
    {
        return Drive.ToggleReverse();
    }

    public CarInfo GetCarInfo()
    {
        return new CarInfo(FrontWheel.DiameterInMeters, RearWheel.DiameterInMeters, Drive.FrontWheelRpm, Drive.RearWheelRpm);
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
        updateShockAbsorber(frontWheelShockAbsorber, frontPart, FrontWheel);
        updateShockAbsorber(rearWheelShockAbsorber, rearPart, RearWheel);
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
        result += FrontWheelCollider.IsTouchingLayers() ? 1 : 0;
        result += RearWheelCollider.IsTouchingLayers() ? 1 : 0;
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

    public GameObject[] GetBrakeables()
    {
        return new GameObject[] { middlePart };
    }
}