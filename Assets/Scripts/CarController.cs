using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class CarController : MonoBehaviour, ICarController
{
    [SerializeField]
    private Car car;

    [SerializeField]
    private PiekaSlider frontWheelSlider;
    public IPiekaSlider FrontWheelSlider { get; set; }

    [SerializeField]
    private PiekaSlider rearWheelSlider;
    public IPiekaSlider RearWheelSlider { get; set; }

    [SerializeField]
    private PiekaSlider frontWheelDampSlider;
    public IPiekaSlider FrontWheelDampSlider { get; set; }

    [SerializeField]
    private PiekaSlider rearWheelDampSlider;
    public IPiekaSlider RearWheelDampSlider { get; set; }

    [SerializeField]
    private PiekaSlider frontRearDriveRatioSlider;
    public IPiekaSlider FrontRearDriveRatioSlider { get; set; }

    [SerializeField]
    private Meter speedMeter;
    public IMeter SpeedMeter { get; set; }

    [SerializeField]
    private Pedal accelerationPedal;

    public IPedal AccelerationPedal { get; set; }

    [SerializeField]
    private Pedal brakePedal;

    public IPedal BrakePedal { get; set; }


    //todo change to common ui element eg. toggle button
    [SerializeField]
    private Reverse reverse;

    void Awake()
    {
        FrontWheelSlider = frontWheelSlider;
        RearWheelSlider = rearWheelSlider;
        FrontWheelDampSlider = frontWheelDampSlider;
        RearWheelDampSlider = rearWheelDampSlider;
        FrontRearDriveRatioSlider = frontRearDriveRatioSlider;
        SpeedMeter = speedMeter;
        AccelerationPedal = accelerationPedal;
        BrakePedal = brakePedal;

        setOnSlideFunctionIfNotNull(FrontRearDriveRatioSlider, (v) => car.Drive.FrontRearRatio = v, "FrontRearDriveRatioSlider");
        setOnSlideFunctionIfNotNull(FrontWheelSlider, (v) => { var j = car.FrontWheel.Joint; var s = j.suspension; s.frequency = v; j.suspension = s; }, "FrontWheelSlider");
        setOnSlideFunctionIfNotNull(RearWheelSlider, (v) => { var j = car.RearWheel.Joint; var s = j.suspension; s.frequency = v; j.suspension = s; }, "RearWheelSlider");
        setOnSlideFunctionIfNotNull(FrontWheelDampSlider, (v) => { var j = car.FrontWheel.Joint; var s = j.suspension; s.dampingRatio = v; j.suspension = s; }, "FrontWheelDampSlider");
        setOnSlideFunctionIfNotNull(RearWheelDampSlider, (v) => { var j = car.RearWheel.Joint; var s = j.suspension; s.dampingRatio = v; j.suspension = s; }, "RearWheelDampSlider");
        if (reverse != null)
        {
            reverse.SetOnReverseToggle(() => car.Drive.ToggleReverse());
        }
        else
        {
            Debug.Log("reverse is null");
        }
    }

    private void setOnSlideFunctionIfNotNull(IPiekaSlider slider, OnSlide onSlide, string name)
    {
        if (slider != null)
        {
            slider.RegisterOnSlide(onSlide);
        }
        else
        {
            Debug.Log(name + " is null");
        }
    }

    void Update()
    {
        if (SpeedMeter != null)
        {
            SpeedMeter.Value = Mathf.Abs(Utils.WheelRpmToKmPerHour(car.Drive.FrontWheelRPM, car.FrontWheel.DiameterInMeters));
        }
        if (AccelerationPedal != null)
        {
            car.Engine.Throttle = AccelerationPedal.Value;
        }
        if (BrakePedal != null)
        {
            car.Drive.Brake(BrakePedal.Value);
        }
    }


}
