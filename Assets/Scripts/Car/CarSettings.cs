using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pieka/CarSettings")]
public class CarSettings : ScriptableObject
{
    public float FrontWheelSuspensionFrequency
    {
        get { return frontWheelSuspensionFrequency; }
        set { frontWheelSuspensionFrequency = value; SEventSystem.FireEvent(EventNames.CAR_SETTINGS_CHANGED); }
    }

    [SerializeField]
    [Range(1, 10)]
    private float frontWheelSuspensionFrequency;

    public float RearWheelSuspensionFrequency
    {
        get { return rearWheelSuspensionFrequency; }
        set { rearWheelSuspensionFrequency = value; SEventSystem.FireEvent(EventNames.CAR_SETTINGS_CHANGED); }
    }
    [SerializeField]
    [Range(1, 10)]
    private float rearWheelSuspensionFrequency;

    public float FrontWheelSuspensionDamping
    {
        get { return frontWheelSuspensionDamping; }
        set { frontWheelSuspensionDamping = value; SEventSystem.FireEvent(EventNames.CAR_SETTINGS_CHANGED); }
    }
    [SerializeField]
    [Range(0, 1)]
    private float frontWheelSuspensionDamping;

    public float RearWheelSuspensionDamping
    {
        get { return rearWheelSuspensionDamping; }
        set { rearWheelSuspensionDamping = value; SEventSystem.FireEvent(EventNames.CAR_SETTINGS_CHANGED); }
    }
    [SerializeField]
    [Range(0, 1)]
    private float rearWheelSuspensionDamping;

    public float FrontWheelSuspensionHeight
    {
        get { return frontWheelSuspensionHeight; }
        set { frontWheelSuspensionHeight = value; SEventSystem.FireEvent(EventNames.CAR_SETTINGS_CHANGED); }
    }
    [SerializeField]
    [Range(1, 3)]
    private float frontWheelSuspensionHeight;

    public float RearWheelSuspensionHeight
    {
        get { return rearWheelSuspensionHeight; }
        set { rearWheelSuspensionHeight = value; SEventSystem.FireEvent(EventNames.CAR_SETTINGS_CHANGED); }
    }
    [SerializeField]
    [Range(1, 3)]
    private float rearWheelSuspensionHeight;

    public float FrontRearDriveRatio
    {
        get { return frontRearDriveRatio; }
        set { frontRearDriveRatio = value; SEventSystem.FireEvent(EventNames.CAR_SETTINGS_CHANGED); }
    }
    [SerializeField]
    [Range(0, 1)]
    private float frontRearDriveRatio;

    public float FrontRearBrakeRatio
    {
        get { return frontRearBrakeRatio; }
        set { frontRearBrakeRatio = value; SEventSystem.FireEvent(EventNames.CAR_SETTINGS_CHANGED); }
    }
    [SerializeField]
    [Range(0, 1)]
    private float frontRearBrakeRatio;
}
