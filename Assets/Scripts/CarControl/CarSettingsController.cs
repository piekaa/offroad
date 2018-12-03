using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

class CarSettingsController : PiekaBehaviour
{

    [SerializeField]
    private CarSettings carSettings;

    [SerializeField]
    private Car car;

    void Start()
    {
        applySettings();
    }

    [OnEvent(EventNames.CAR_SETTINGS_CHANGED)]
    private void applySettings()
    {
        car.SetFrontSuspensionFrequency(carSettings.FrontWheelSuspensionFrequency);
        car.SetRearSuspensionFrequency(carSettings.RearWheelSuspensionFrequency);

        car.SetFrontDampingRatio(carSettings.FrontWheelSuspensionDamping);
        car.SetRearDampingRatio(carSettings.RearWheelSuspensionDamping);

        car.SetFrontSuspensionHeight(carSettings.FrontWheelSuspensionHeight);
        car.SetRearSuspensionHeight(carSettings.RearWheelSuspensionHeight);

        car.SetFrontRearDriveRatio(carSettings.FrontRearDriveRatio);
        car.SetFrontRearBrakeRatio(carSettings.FrontRearBrakeRatio);
    }

}