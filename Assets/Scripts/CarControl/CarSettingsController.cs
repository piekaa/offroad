using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

class CarSettingsController : MonoBehaviourWithFirstFrameCallback
{
    public Car Car;

    public PiekaSlider FrontWheelSlider;

    public PiekaSlider RearWheelSlider;

    public PiekaSlider FrontWheelDampSlider;

    public PiekaSlider RearWheelDampSlider;

    public PiekaSlider FrontSuspensionHeightSlider;

    public PiekaSlider RearSuspensionHeightSlider;

    public PiekaSlider FrontRearDriveRatioSlider;

    public PiekaSlider FrontRearBrakeRatioSlider;

    protected override void OnFirstFrame()
    {
        setOnSlideFunctionAndInvokeIfNotNull(FrontWheelSlider, (v) => Car.SetFrontSuspensionFrequency(v), "FrontWheelSlider");
        setOnSlideFunctionAndInvokeIfNotNull(RearWheelSlider, (v) => Car.SetRearSuspensionFrequency(v), "RearWheelSlider");
        setOnSlideFunctionAndInvokeIfNotNull(FrontWheelDampSlider, (v) => Car.SetFrontDampingRatio(v), "FrontWheelDampSlider");
        setOnSlideFunctionAndInvokeIfNotNull(RearWheelDampSlider, (v) => Car.SetRearDampingRatio(v), "RearWheelDampSlider");
        setOnSlideFunctionAndInvokeIfNotNull(FrontSuspensionHeightSlider, (v) => Car.SetFrontSuspensionHeight(v), "FrontSuspensionHeightSlider");
        setOnSlideFunctionAndInvokeIfNotNull(RearSuspensionHeightSlider, (v) => Car.SetRearSuspensionHeight(v), "RearSuspensionHeightSlider");
        setOnSlideFunctionAndInvokeIfNotNull(FrontRearDriveRatioSlider, (v) => Car.SetFrontRearDriveRatio(v), "FrontRearDriveRatioSlider");
        setOnSlideFunctionAndInvokeIfNotNull(FrontRearBrakeRatioSlider, (v) => Car.SetFrontRearBrakeRatio(v), "FrontRearBrakeRatioSlider");
    }

    private void setOnSlideFunctionAndInvokeIfNotNull(PiekaSlider slider, RunFloat onSlide, string name)
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