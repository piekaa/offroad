using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    [SerializeField]
    private Car car;

    [SerializeField]
    private PiekaSlider frontWheelSlider;
    public IPiekaSlider FrontWheelSlider;

    [SerializeField]
    private PiekaSlider rearWheelSlider;
    public IPiekaSlider RearWheelSlider;

    [SerializeField]
    private PiekaSlider frontWheelDampSlider;
    public IPiekaSlider FrontWheelDampSlider;

    [SerializeField]
    private PiekaSlider rearWheelDampSlider;
    public IPiekaSlider RearWheelDampSlider;

    [SerializeField]
    private PiekaSlider frontRearDriveRatioSlider;
    public IPiekaSlider FrontRearDriveRatioSlider;

    [SerializeField]
    private Meter speedMeter;
    public IMeter SpeedMeter;

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

        FrontRearDriveRatioSlider.RegisterOnSlide((v) => car.Drive.FrontRearRatio = v);
        FrontWheelSlider.RegisterOnSlide((v) => { var j = car.FrontWheel.Joint; var s = j.suspension; s.frequency = v; j.suspension = s; });
        RearWheelSlider.RegisterOnSlide((v) => { var j = car.RearWheel.Joint; var s = j.suspension; s.frequency = v; j.suspension = s; });
        FrontWheelDampSlider.RegisterOnSlide((v) => { var j = car.FrontWheel.Joint; var s = j.suspension; s.dampingRatio = v; j.suspension = s; });
        RearWheelDampSlider.RegisterOnSlide((v) => { var j = car.RearWheel.Joint; var s = j.suspension; s.dampingRatio = v; j.suspension = s; });
        reverse.SetOnReverseToggle(() => car.Drive.ToggleReverse());
    }

    void Update()
    {
        SpeedMeter.Value = Mathf.Abs(Utils.WheelRpmToKmPerHour(car.Drive.FrontWheelRPM, car.FrontWheel.DiameterInMeters));
    }
}
