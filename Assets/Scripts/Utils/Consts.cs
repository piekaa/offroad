using UnityEngine;

public class Consts
{
    public static Vector3 NOWHERE = new Vector3(-100000, -100000, -10000);

    public const int CarLayer = 8;
    public const int CarLayerMask = 1 << 8;
    public const int FloorLayer = 9;
    public const int FloorLayerMask = 1 << 9;
    public const int LaggageLayer = 10;
    public const int LaggageLayerMask = 1 << 10;

    public const string FrontWheelTag = "FrontWheel";

    public const float MetersPerWroldUnit = 0.42f;

}