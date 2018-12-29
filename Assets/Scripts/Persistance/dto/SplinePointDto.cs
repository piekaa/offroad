using UnityEngine;
using UnityEngine.U2D;

public class SplinePointDto
{
    public Vector3Dto Position;
    public Vector3Dto LeftTangent;
    public Vector3Dto RightTangent;
    public ShapeTangentMode TangentMode;

    public SplinePointDto(Vector3 position, Vector3 leftTangent, Vector3 rightTangent, ShapeTangentMode tangentMode)
    {
        Position = new Vector3Dto(position);
        LeftTangent = new Vector3Dto(leftTangent);
        RightTangent = new Vector3Dto(rightTangent);
        TangentMode = tangentMode;
    }

    public SplinePointDto()
    {
    }
}