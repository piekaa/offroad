using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriveController : MonoBehaviourWithFirstFrameCallback, ICarDriveController
{
    [SerializeField]
    private Car car;
    public ICar Car;

    [SerializeField]
    private Pedal accelerationPedal;
    public IPedal AccelerationPedal { get; set; }

    [SerializeField]
    private Pedal brakePedal;

    public IPedal BrakePedal { get; set; }

    [SerializeField]
    private Meter speedMeter;
    public IMeter SpeedMeter { get; set; }

    //todo change to common ui element eg. toggle button
    [SerializeField]
    private Reverse reverse;

    void Awake()
    {
        Car = car;

        AccelerationPedal = accelerationPedal;
        BrakePedal = brakePedal;
        SpeedMeter = speedMeter;


    }

    protected override void OnFirstFrame()
    {
        registerOnPedalIsPressedIfNotNull(AccelerationPedal, (v) => Car.Engine.Throttle = v, "AccelerationPedal");
        registerOnPedalIsPressedIfNotNull(BrakePedal, (v) => Car.Brake(v), "BrakePedal");

        if (reverse != null)
        {
            reverse.SetOnReverseToggle(() => Car.Drive.ToggleReverse());
        }
        else
        {
            Debug.Log("reverse is null");
        }
    }

    private void registerOnPedalIsPressedIfNotNull(IPedal pedal, OnIsPressed onIsPressed, string name)
    {
        if (pedal != null)
        {
            pedal.RegisterOnIsPressed(onIsPressed);
        }
        else
        {
            Debug.Log(name + " is null");
        }
    }

    protected override void Update()
    {
        base.Update();
        if (SpeedMeter != null)
        {
            SpeedMeter.Value = Mathf.Abs(Utils.WheelRpmToKmPerHour(Car.Drive.FrontWheelRPM, Car.FrontWheel.DiameterInMeters));
        }
    }
}
