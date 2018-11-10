using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriveController : MonoBehaviour, ICarDriveController
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

        if (reverse != null)
        {
            reverse.SetOnReverseToggle(() => Car.Drive.ToggleReverse());
        }
        else
        {
            Debug.Log("reverse is null");
        }
    }

    void Update()
    {
        if (SpeedMeter != null)
        {
            SpeedMeter.Value = Mathf.Abs(Utils.WheelRpmToKmPerHour(Car.Drive.FrontWheelRPM, Car.FrontWheel.DiameterInMeters));
        }
        if (AccelerationPedal != null)
        {
            Car.Engine.Throttle = AccelerationPedal.Value;
        }
        if (BrakePedal != null)
        {
            Car.Drive.Brake(BrakePedal.Value);
        }
    }
}
