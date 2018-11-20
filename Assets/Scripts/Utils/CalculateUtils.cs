using UnityEngine;

namespace Pieka.Utils
{
    public class CalculateUtils
    {
        public static float WheelRpmToKmPerHour(float wheelRPM, float wheelDiameterInMeters)
        {
            return wheelRPM * wheelDiameterInMeters * 60 * 3.14f / 1000;
        }

        public static float UnitsPerSecondToKmPerH(float unitsPerSecond)
        {
            return unitsPerSecond * 1.512f;
        }

        // /// <summary>
        // /// (0,1) = 0, (-1,0)- 90, (-1,-1) = 180, (1,0) = 270
        // /// </summary>
        // /// <param name="vector"></param>
        // /// <returns></returns>
        // public static float Angle(Vector2 vector)
        // {
        //     vector.Normalize();
        //     if (vector.x == 0 && vector.y == 1)
        //     {
        //         return 0;
        //     }
        //     if (vector.x == -1 && vector.y == 0)
        //     {
        //         return 90;
        //     }
        //     if (vector.x == -1 && vector.y == -1)
        //     {
        //         return 180;
        //     }
        //     if (vector.x == 1 && vector.y == 0)
        //     {
        //         return 270;
        //     }

        //     if (vector.x < 0 && vector.y > 0)
        //     {

        //     }

        // }
    }
}