﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSettingsMenuUI : MonoBehaviour
{

    [SerializeField]
    private CarSettings carSettings;

    [SerializeField]
    private PiekaSlider frontWheelSuspensionFrequency;

    [SerializeField]
    private PiekaSlider rearWheelSuspensionFrequency;

    [SerializeField]
    private PiekaSlider frontWheelSuspensionHeight;

    [SerializeField]
    private PiekaSlider fearWheelSuspensionHeight;

    [SerializeField]
    private PiekaSlider frontRearDriveRatio;

    [SerializeField]
    private PiekaSlider frontRearBrakeRatio;

    void Start()
    {
        SetValues();
        frontWheelSuspensionFrequency.RegisterOnSlide(v => carSettings.FrontWheelSuspensionFrequency = v);
        rearWheelSuspensionFrequency.RegisterOnSlide(v => carSettings.RearWheelSuspensionFrequency = v);
        frontWheelSuspensionHeight.RegisterOnSlide(v => carSettings.FrontWheelSuspensionHeight = v);
        fearWheelSuspensionHeight.RegisterOnSlide(v => carSettings.RearWheelSuspensionHeight = v);
        frontRearDriveRatio.RegisterOnSlide(v => carSettings.FrontRearDriveRatio = v);
        frontRearBrakeRatio.RegisterOnSlide(v => carSettings.FrontRearBrakeRatio = v);
    }

    private void SetValues()
    {
        frontWheelSuspensionFrequency.Value = carSettings.FrontWheelSuspensionFrequency;
        rearWheelSuspensionFrequency.Value = carSettings.RearWheelSuspensionFrequency;
        frontWheelSuspensionHeight.Value = carSettings.FrontWheelSuspensionHeight;
        fearWheelSuspensionHeight.Value = carSettings.RearWheelSuspensionHeight;
        frontRearDriveRatio.Value = carSettings.FrontRearDriveRatio;
        frontRearBrakeRatio.Value = carSettings.FrontRearBrakeRatio;
    }
}
