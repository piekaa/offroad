using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class CarSettingsController : MonoBehaviour, ICarSettingsController
{
    [SerializeField]
    private Car car;
    public ICar Car;

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
    private PiekaSlider frontSuspensionHeightSlider;
    public IPiekaSlider FrontSuspensionHeightSlider { get; set; }

    [SerializeField]
    private PiekaSlider rearSuspensionHeightSlider;
    public IPiekaSlider RearSuspensionHeightSlider { get; set; }

    [SerializeField]
    private PiekaSlider frontRearDriveRatioSlider;
    public IPiekaSlider FrontRearDriveRatioSlider { get; set; }

    void Awake()
    {
        FrontWheelSlider = frontWheelSlider;
        RearWheelSlider = rearWheelSlider;
        FrontWheelDampSlider = frontWheelDampSlider;
        RearWheelDampSlider = rearWheelDampSlider;
        FrontRearDriveRatioSlider = frontRearDriveRatioSlider;
        FrontSuspensionHeightSlider = frontSuspensionHeightSlider;
        RearSuspensionHeightSlider = rearSuspensionHeightSlider;
        Car = car;

        setOnSlideFunctionIfNotNull(FrontRearDriveRatioSlider, (v) => Car.Drive.FrontRearRatio = v, "FrontRearDriveRatioSlider");
        setOnSlideFunctionIfNotNull(FrontWheelSlider, (v) => Car.SetFrontSuspensionFrequency(v), "FrontWheelSlider");
        setOnSlideFunctionIfNotNull(RearWheelSlider, (v) => Car.SetRearSuspensionFrequency(v), "RearWheelSlider");
        setOnSlideFunctionIfNotNull(FrontWheelDampSlider, (v) => Car.SetFrontDampingRatio(v), "FrontWheelDampSlider");
        setOnSlideFunctionIfNotNull(RearWheelDampSlider, (v) => Car.SetRearDampingRatio(v), "RearWheelDampSlider");
        setOnSlideFunctionIfNotNull(FrontSuspensionHeightSlider, (v) => Car.SetFrontSuspensionHeight(v), "FrontSuspensionHeightSlider");
        setOnSlideFunctionIfNotNull(RearSuspensionHeightSlider, (v) => Car.SetRearSuspensionHeight(v), "RearSuspensionHeightSlider");
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
}
