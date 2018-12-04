using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Pieka/Drivers/PlayerDriver")]
public class PlayerDriver : Driver
{

    private float acceleration;
    private float brake;

    [OnEvent(EventNames.ACCELERATION_PEDAL)]
    private void onAcceleration(string id, PMEventArgs args)
    {
        acceleration = args.Floating;
    }

    [OnEvent(EventNames.BRAKE_PEDAL)]
    private void onBrake(string id, PMEventArgs args)
    {
        brake = args.Floating;
    }


    public override float Acceleration()
    {
        return acceleration;
    }

    public override float Brake()
    {
        return brake;
    }
}
