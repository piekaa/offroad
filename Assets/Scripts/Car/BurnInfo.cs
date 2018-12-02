using UnityEngine;

public struct BurnInfo
{
    public BurnInfo.WhichWheelEnum WhichWheel;

    public BurnInfo.DirectionEnum Direction;

    public Vector2 Point;

    /// <summary>
    /// 0-1
    /// </summary>
    public float Power;


    public GameObject OtherGameObject;

    public PiekaMaterial WheelMaterial;

    public BurnInfo(WhichWheelEnum whichWheel, DirectionEnum direction, Vector2 point, float power, GameObject otherGameObject, PiekaMaterial wheelMaterial)
    {
        WhichWheel = whichWheel;
        Direction = direction;
        Point = point;
        Power = power;
        OtherGameObject = otherGameObject;
        WheelMaterial = wheelMaterial;
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