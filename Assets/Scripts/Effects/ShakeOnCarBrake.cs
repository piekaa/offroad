using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeOnCarBrake : PiekaBehaviour
{

    private CameraShake cameraShake;

    void Awake()
    {
        cameraShake = GetComponent<CameraShake>();
    }

    public float MaximumShakeVelocity = 30;
    public float MinimumShakeVelocity = 10;

    [OnEvent(EventNames.CAR_BRAKE_WITH_VELOCITY)]
    public void onCarBrake(string id, PMEventArgs args)
    {
        var velocity = args.Floating;
        var power = Mathf.Clamp(velocity / MaximumShakeVelocity, 0, 1);
        UnityEngine.Debug.Log(power);
        cameraShake.Shake(power);
    }
}
