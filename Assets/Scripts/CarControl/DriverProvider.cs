using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverProvider : PiekaBehaviour
{

    [SerializeField]
    private Driver playerDriver;

    [SerializeField]
    private Driver parkingDriver;

    [SerializeField]
    private Driver idleDriver;

    private DriverController driverController;

    void Awake()
    {
        driverController = GetComponent<DriverController>();
    }

    [OnEvent(EventNames.FIRST_ACCELERATION_PEDAL_USE)]
    private void onFirstAccelPedalUse()
    {
        driverController.Driver = playerDriver;
    }

    [OnEvent(EventNames.RESET)]
    private void onReset()
    {
        driverController.Driver = parkingDriver;
    }

    [OnEvent(EventNames.CAR_BRAKE)]
    private void onCarBrake()
    {
        driverController.Driver = idleDriver;
    }
}
