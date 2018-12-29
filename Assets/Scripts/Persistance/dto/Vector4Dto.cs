using UnityEngine;

public class Vector4Dto
{
    public float x;
    public float y;
    public float z;
    public float w;

    public Vector4Dto()
    {
    }

    public Vector4Dto(Vector4 vector4)
    {
        x = vector4.x;
        y = vector4.y;
        z = vector4.z;
        w = vector4.w;
    }

    public Vector4 ToVector4()
    {
        return new Vector4(x, y, z, w);
    }
}