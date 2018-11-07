using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteUtils
{
    //Doesn't handle rotation and pivot
    public static SpritePositions getWolrdPositions(SpriteRenderer spriteRenderer)
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
}
