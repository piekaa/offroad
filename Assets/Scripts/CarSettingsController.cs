using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class CarSettingsController : MonoBehaviourWithFirstFrameCallback, ICarSettingsController
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


    }

    protected override void OnFirstFrame()
    {
        setOnSlideFunctionAndInvokeIfNotNull(FrontRearDriveRatioSlider, (v) => Car.Drive.FrontRearRatio = v, "FrontRearDriveRatioSlider");
        setOnSlideFunctionAndInvokeIfNotNull(FrontWheelSlider, (v) => Car.SetFrontSuspensionFrequency(v), "FrontWheelSlider");
        setOnSlideFunctionAndInvokeIfNotNull(RearWheelSlider, (v) => Car.SetRearSuspensionFrequency(v), "RearWheelSlider");
        setOnSlideFunctionAndInvokeIfNotNull(FrontWheelDampSlider, (v) => Car.SetFrontDampingRatio(v), "FrontWheelDampSlider");
        setOnSlideFunctionAndInvokeIfNotNull(RearWheelDampSlider, (v) => Car.SetRearDampingRatio(v), "RearWheelDampSlider");
        setOnSlideFunctionAndInvokeIfNotNull(FrontSuspensionHeightSlider, (v) => Car.SetFrontSuspensionHeight(v), "FrontSuspensionHeightSlider");
        setOnSlideFunctionAndInvokeIfNotNull(RearSuspensionHeightSlider, (v) => Car.SetRearSuspensionHeight(v), "RearSuspensionHeightSlider");
    }

    private void setOnSlideFunctionAndInvokeIfNotNull(IPiekaSlider slider, OnSlide onSlide, string name)
    {
        if (slider != null)
        {
            slider.RegisterOnSlide(onSlide);
            onSlide(slider.Value);
        }
        else
        {
            Debug.Log(name + " is null");
        }
    }
}
