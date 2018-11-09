using UnityEngine;

public interface IWheel
{
    void AddTorque(float torque);

    float AngularDrag { get; set; }

    float AngularVelocity { get; set; }

    //todo questionable
    void SetMotorSpeed(float speed);

    //todo questionable
    void SetUseMotor(bool useMotor);

    float DiameterInMeters { get; }
}