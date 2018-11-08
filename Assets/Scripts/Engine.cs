using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Temp implementation
public class Engine : OrderedScript
{
    public float RPM { get; private set; }
    public float Torque { get; private set; }
    public float Drag { set; private get; }
    /// <summary>
    /// 0-1
    /// </summary>
    private float throttle;

    private bool clutchEngaged;

    [SerializeField]
    private int cylinders = 4;

    [SerializeField]
    private float explosionPower = 1f;

    [SerializeField]
    private Meter speedMeter;

    [SerializeField]
    public float maxSpeed;

    [SerializeField]
    private Pedal accelerationPedal;

    public IPedal AccelerationPedal;

    [SerializeField]
    private Drive drive;

    private bool reverse;

    //todo should be in wheel
    private float wheelDiameterInMeters = 0.5f;

    void Awake()
    {
        AccelerationPedal = accelerationPedal;
    }

    void Start()
    {
        Torque = 0.1f;
        Drag = 10;
        throttle = 0;
    }

    public override void OrderedFixedUpdate()
    {
        float wheelRPM = Mathf.Abs(drive.FrontWheelRPM);

        float speedInKmPerHour = wheelRpmToKmPerHour(wheelRPM, wheelDiameterInMeters);
        speedMeter.Value = speedInKmPerHour;

        throttle = AccelerationPedal.Value;
        float currentExpPower = cylinders * throttle * explosionPower;
        if (speedInKmPerHour < maxSpeed)
        {
            var sign = reverse ? -1 : 1;
            drive.AccelerateFront(currentExpPower * sign);
            drive.AccelerateRear(currentExpPower * sign);
        }
    }

    //true if engine is on reverse
    public bool ToggleReverse()
    {
        float frontWheelRPM = Mathf.Abs(drive.FrontWheelRPM);
        float rearWheelRPM = Mathf.Abs(drive.RearWheelRPM);
        float frontWheelKmPerHour = wheelRpmToKmPerHour(frontWheelRPM, wheelDiameterInMeters);
        float rearWheelKmPerHour = wheelRpmToKmPerHour(rearWheelRPM, wheelDiameterInMeters);

        if (frontWheelKmPerHour < 5 && rearWheelKmPerHour < 5)
        {
            reverse = !reverse;
        }
        return reverse;
    }

    private float wheelRpmToKmPerHour(float wheelRPM, float wheelDiameterInMeters)
    {
        return wheelRPM * wheelDiameterInMeters * 60 * 3.14f / 1000;
    }

}
