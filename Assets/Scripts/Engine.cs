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
    private float explosionPower =1f;

    [SerializeField]
    private Meter tachometer;

    [SerializeField]
    private Meter speedMeter;

    void Start()
    {
        Torque = 0.1f;
        Drag = 10;
        throttle = 0;
    }

    [SerializeField]
    private Pedal accelerationPedal;

    [SerializeField]
    private Drive drive;


    public override void OrderedFixedUpdate()
    {
        float WRPM = Mathf.Abs(drive.FrontWheelRPM);

        //todo
        float wheelDiameterInMeters = 0.5f;
        float speedInKmPerHour = WRPM * wheelDiameterInMeters * 60 * 3.14f / 1000;
        speedMeter.Value = speedInKmPerHour;

        RPM = WRPM;
        if (RPM < 10)
        {
            RPM = 10;
        }
        throttle = accelerationPedal.Value;
        Debug.Log(explosionPower);
        float currentExpPower = cylinders * throttle * explosionPower;
        Debug.Log(currentExpPower);
        drive.AccelerateFront(currentExpPower);
    }

    void Update()
    {
        tachometer.Value = RPM / 1000;
    }


}
