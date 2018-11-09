public interface IDrive
{
    // 0 means rear wheel drive, 1 means front wheel drive
    float FrontRearRatio { get; set; }
    //true if engine is on reverse
    bool ToggleReverse();

    float FrontWheelRPM { get; }

    float RearWheelRPM { get; }

    void SetFrontWheel(IWheel wheel);

    void SetRearWheel(IWheel wheel);

    void Accelerate(float power);

    /// <summary>
    /// Invoke only in FixedUpdate
    /// </summary>
    /// <param name="power">0-1</param>
    void Brake(float power);
}