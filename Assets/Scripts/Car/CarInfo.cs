public class CarInfo
{
    public float FrontWheelDiameterInMeters { get; private set; }

    public float RearWheelDiameterInMeters { get; private set; }

    public float FrontWheelRpm { get; private set; }

    public float RearWheelRpm { get; private set; }

    public CarInfo(float frontWheelDiameterInMeters, float rearWheelDiameterInMeters, float frontWheelRpm, float rearWheelRpm)
    {
        FrontWheelDiameterInMeters = frontWheelDiameterInMeters;
        RearWheelDiameterInMeters = rearWheelDiameterInMeters;
        FrontWheelRpm = frontWheelRpm;
        RearWheelRpm = rearWheelRpm;
    }
}