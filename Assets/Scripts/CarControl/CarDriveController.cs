using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CarDriveController : MonoBehaviourWithFirstFrameCallback
{
    public Car Car;

    public Pedal AccelerationPedal;

    public Pedal BrakePedal;

    public Meter SpeedMeter;

    public ToggleButton ReverseToggleButton;

    protected override void OnFirstFrame()
    {
        registerOnPedalIsPressedIfNotNull(AccelerationPedal, (v) => Car.Accelerate(v), "AccelerationPedal");
        registerOnPedalIsPressedIfNotNull(BrakePedal, (v) => Car.Brake(v), "BrakePedal");

        if (ReverseToggleButton != null)
        {
            ReverseToggleButton.SetOnToggle(() => Car.ToggleReverse());
        }
        else
        {
            Debug.Log("reverse is null");
        }
    }

    private void registerOnPedalIsPressedIfNotNull(Pedal pedal, RunFloat onIsPressed, string name)
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
            var carInfo = Car.GetCarInfo();
            SpeedMeter.Value = Mathf.Abs(CalculateUtils.WheelRpmToKmPerHour(carInfo.FrontWheelRpm, carInfo.FrontWheelDiameterInMeters));
        }
    }
}