using System.Collections.Generic;
using UnityEngine.U2D;

public class SplineDto
{
    public List<SplinePointDto> Points;
    public bool openEnded;

    public SplineDto()
    {
    }

    public SplineDto(Spline spline)
    {
        openEnded = spline.isOpenEnded;
        Points = new List<SplinePointDto>(spline.GetPointCount());
        for (var i = 0; i < spline.GetPointCount(); i++)
        {
            Points.Add(new SplinePointDto(spline.GetPosition(i), spline.GetLeftTangent(i), spline.GetRightTangent(i), spline.GetTangentMode(i)));
        }
    }

    public override string ToString()
    {
        return string.Format("Points: {0}", Points);
    }
}