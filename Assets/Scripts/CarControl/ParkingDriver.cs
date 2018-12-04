using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Pieka/Drivers/ParkingDriver")]
public class ParkingDriver : Driver
{
    public override float Acceleration()
    {
        return 0;
    }

    public override float Brake()
    {
        return 1;
    }
}
