using UnityEngine;

public interface IDrive
{

    /// <value>0 means rear wheel drive, 1 means front wheel drive</value>
    float FrontRearRatio { get; set; }

    /// <returns>true if engine is on reverse</returns>
    bool ToggleReverse();

    float FrontWheelRPM { get; }

    float RearWheelRPM { get; }

    void SetFrontWheel(IWheel wheel);

    void SetRearWheel(IWheel wheel);

    /// <summary>
    /// Accelerates the front. Invoke only in FixedUpdate
    /// </summary>
    /// <param name="power">Torque</param>
    void Accelerate(float power);

    /// <summary>
    /// Invoke only in FixedUpdate
    /// </summary>
    /// <param name="power">0-1</param>
    void Brake(float power);

    void SetJoints(WheelJoint2D front, WheelJoint2D back);
}