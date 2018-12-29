using UnityEngine;

[CreateAssetMenu(menuName = "Pieka/Events")]
public class EventNames : ScriptableObject
{
    public const string TEST = "test";
    public const string WHEEL_BURN = "wheelBurn";
    public const string CAR_SETTINGS_CHANGED = "carSettingsChanged";
    public const string RESET = "reset";
    public const string FIRST_ACCELERATION_PEDAL_USE = "firstAccelerationPedalUse";
    public const string ACCELERATION_PEDAL = "accelerationPedal";
    public const string BRAKE_PEDAL = "brakePedal";
    public const string CAR_BRAKE = "carBrake";
    public const string CAR_BRAKE_WITH_VELOCITY = "carBrakeWithVelocity";
    public const string REVERSE_ON = "reverseOn";
    public const string REVERSE_OFF = "reverseOff";
    public const string LEVEL_INSTANTIATED = "levelInstantiated";

    private string[] events;

    private static string[] allEvents =
    {
        TEST,
        WHEEL_BURN,
        CAR_SETTINGS_CHANGED,
        RESET,
        FIRST_ACCELERATION_PEDAL_USE,
        ACCELERATION_PEDAL,
        BRAKE_PEDAL,
        CAR_BRAKE,
        CAR_BRAKE_WITH_VELOCITY,
        REVERSE_ON,
        REVERSE_OFF,
        LEVEL_INSTANTIATED,
    };

    public string[] Events
    {
        get { return events; }
    }

    void OnEnable()
    {
        events = allEvents;
    }
}