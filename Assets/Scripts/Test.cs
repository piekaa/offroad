using UnityEngine;

public class Test : PiekaBehaviour
{

    public CameraShake cameraShake;

    [Range(0, 1)]
    public float power = 0;

    [OnEvent(EventNames.TEST)]
    void test()
    {
        cameraShake.Shake(power);
    }

}