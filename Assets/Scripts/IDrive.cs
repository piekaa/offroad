public interface IDrive
{
    // 0 means rear wheel drive, 1 means front wheel drive
    float FrontRearRatio { get; set; }
    //true if engine is on reverse
    bool ToggleReverse();

    float FrontWheelSpeed { get; }

    float RearWheelSpeed { get; }

    float FrontWheelRPM { get; }

    float RearWheelRPM { get; }
}