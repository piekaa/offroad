using UnityEngine;

namespace Pieka.Car
{
    public struct BurnInfo
    {
        public BurnInfo.WhichWheelEnum WhichWheel;

        public BurnInfo.DirectionEnum Direction;

        public Vector2 Point;

        /// <summary>
        /// 0-1
        /// </summary>
        public float Power;

        public BurnInfo(BurnInfo.WhichWheelEnum whichWheel, BurnInfo.DirectionEnum direction, Vector2 point, float power)
        {
            WhichWheel = whichWheel;
            Direction = direction;
            Point = point;
            Power = power;
        }

        public enum WhichWheelEnum
        {
            FRONT,
            REAR
        }

        public enum DirectionEnum
        {
            LEFT,
            RIGHT
        }
    }
}