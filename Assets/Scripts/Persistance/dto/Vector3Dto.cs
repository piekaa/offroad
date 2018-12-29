using UnityEngine;

public class Vector3Dto
{
    public float x;
    public float y;
    public float z;

    public Vector3Dto()
    {

    }

    public Vector3Dto(Vector3 v)
    {
        x = v.x;
        y = v.y;
        z = v.z;
    }

    public Vector3Dto(Quaternion q) : this(q.eulerAngles)
    {
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x,y,z);
    }
    
    public Quaternion ToQuaterion()
    {
        return Quaternion.Euler(x,y,z);
    }
}