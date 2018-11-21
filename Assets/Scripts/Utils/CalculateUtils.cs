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
    }
}