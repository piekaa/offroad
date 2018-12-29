using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Pieka/Drivers/IdleDriver")]
public class IdleDriver : Driver
{
    public override float Acceleration()
    {
        return 0;
    }

    public override float Brake()
    {
        return 0;
    }
}
