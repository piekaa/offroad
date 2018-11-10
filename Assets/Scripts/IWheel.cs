using UnityEngine;

public interface IWheel
{
    void AddTorque(float torque);

    float AngularDrag { get; set; }

    float AngularVelocity { get; set; }

    float DiameterInMeters { get; }
}