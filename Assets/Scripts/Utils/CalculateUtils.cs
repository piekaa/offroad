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

        public static float Vector2ToAngle(Vector2 vector)
        {
            float angle = 0;
            if (vector.x <= 0)
            {
                angle = Vector2.Angle(new Vector2(0, 1), vector);
            }
            else
            {
                angle = Vector2.Angle(new Vector2(0, -1), vector);
            }
            return angle;
        }
    }
}