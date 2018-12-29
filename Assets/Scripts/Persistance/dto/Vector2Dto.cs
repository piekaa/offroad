using UnityEngine;

public class Vector2Dto
{
    public float x;
    public float y;

    public Vector2Dto()
    {
    }

    public Vector2Dto(Vector2 v)
    {
        x = v.x;
        y = v.y;
    }

    public Vector2 ToVector2()
    {
        return new Vector2(x, y);
    }
}