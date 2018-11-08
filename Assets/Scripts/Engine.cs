using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //todo move pedal to controller, create IEngine
    [SerializeField]
    private Pedal accelerationPedal;

    public IPedal AccelerationPedal;

    [SerializeField]
    private Drive drive;

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
        throttle = AccelerationPedal.Value;
        float currentExpPower = cylinders * throttle * explosionPower;
        drive.Accelerate(currentExpPower);
    }
}
