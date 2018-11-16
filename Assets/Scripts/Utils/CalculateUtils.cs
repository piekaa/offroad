namespace Pieka.Utils
{
    public class CalculateUtils
    {
        public static float WheelRpmToKmPerHour(float wheelRPM, float wheelDiameterInMeters)
        {
            return wheelRPM * wheelDiameterInMeters * 60 * 3.14f / 1000;
        }
    }
}