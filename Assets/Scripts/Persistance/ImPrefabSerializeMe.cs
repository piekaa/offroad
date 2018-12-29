using System;
using UnityEditor;
using UnityEngine;

public class ImPrefabSerializeMe : MonoBehaviour
{
    [SerializeField]
    private GameObject Prefab;

    public String PrefabPath()
    {
        return AssetDatabase.GetAssetPath(Prefab);
    }
}