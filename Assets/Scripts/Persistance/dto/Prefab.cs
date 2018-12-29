using UnityEditor;
using UnityEngine;

public class Prefab : DtoWithTransform
{
    public string PrefabPath;

    public Prefab()
    {
    }

    public Prefab(ImPrefabSerializeMe imPrefabSerializeMe)
    {
        SetTransform(imPrefabSerializeMe.transform);
        PrefabPath = imPrefabSerializeMe.PrefabPath();
    }
}