using UnityEngine;
using System.Collections.Generic;
public class EffectData
{
    public Vector2 Position;

    public float Angle;

    public Transform Transform;

    public Transform OtherTransform;

    public Dictionary<string, object> Map = new Dictionary<string, object>();

    public EffectData() { }

    public EffectData(string name, object data)
    {
        Map[name] = data;
    }
}