using UnityEngine;
using UnityEngine.U2D;

namespace Pieka.Utils
{
    public class SpriteShapeCalculator
    {
        public static float Angle(SpriteShapeController spriteShapeController, int pointIndex)
        {
            var spline = spriteShapeController.spline;
            var pointsCount = spline.GetPointCount();

            if (pointIndex == 0)
            {
                return CalculateUtils.Vector2ToAngle(spline.GetPosition(1) - spline.GetPosition(0));
            }
            if (pointIndex == pointsCount - 1)
            {
                return CalculateUtils.Vector2ToAngle(spline.GetPosition(pointIndex) - spline.GetPosition(pointIndex - 1));
            }
            return CalculateUtils.Vector2ToAngle(spline.GetPosition(pointIndex + 1) - spline.GetPosition(pointIndex - 1));
        }

        public static float FindStartingPointIndex(SpriteShapeController spriteShapeController, int closestPointIndex, Vector2 position)
        {
            var spline = spriteShapeController.spline;
            var pointsCount = spline.GetPointCount();
            var closestPointPosition = spline.GetPosition(closestPointIndex);
            var angle = Angle(spriteShapeController, closestPointIndex);

            if (closestPointIndex == 0)
            {
                return 0;
            }
            if (closestPointIndex == pointsCount - 1)
            {
                return closestPointIndex - 1;
            }

            if (angle >= 315 || angle < 45)
            {
                if (position.x >= closestPointPosition.x)
                {
                    return closestPointIndex;
                }
                else
                {
                    return closestPointIndex - 1;
                }
            }
            else if (angle >= 45 && angle < 135)
            {
                if (position.y >= closestPointPosition.y)
                {
                    return closestPointIndex;
                }
                else
                {
                    return closestPointIndex - 1;
                }
            }
            else if (angle >= 135 && angle < 225)
            {
                if (position.x < closestPointPosition.x)
                {
                    return closestPointIndex;
                }
                else
                {
                    return closestPointIndex - 1;
                }
            }
            else if (angle >= 225 && angle < 315)
            {
                if (position.y < closestPointPosition.y)
                {
                    return closestPointIndex;
                }
                else
                {
                    return closestPointIndex - 1;
                }
            }
            return -1;
        }
    }
}