using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pieka.Utils
{
    public class SpriteUtils
    {
        //Doesn't handle rotation and pivot
        public static SpritePositions GetWolrdPositions(SpriteRenderer spriteRenderer)
        {
            //todo handle pivot
            //todo handle rotation

            var bottomLeft = spriteRenderer.bounds.min;
            var topRight = spriteRenderer.bounds.max;
            var bottomRight = topRight;
            bottomRight.y = bottomLeft.y;
            var topLeft = bottomLeft;
            topLeft.y = topRight.y;
            var center = (bottomLeft + topRight) / 2;
            return new SpritePositions(topLeft, topRight, bottomLeft, bottomRight, center);
        }

        public static SpritePositions GetWolrdPositions(Sprite sprite)
        {
            //todo handle pivot
            //todo handle rotation

            var bottomLeft = sprite.bounds.min;
            var topRight = sprite.bounds.max;
            var bottomRight = topRight;
            bottomRight.y = bottomLeft.y;
            var topLeft = bottomLeft;
            topLeft.y = topRight.y;
            var center = (bottomLeft + topRight) / 2;
            return new SpritePositions(topLeft, topRight, bottomLeft, bottomRight, center);
        }
    }
}