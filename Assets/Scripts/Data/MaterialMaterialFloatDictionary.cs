using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MaterialMaterialFloatDictionary : Dictionary<MaterialMaterialPair, float>, ISerializationCallbackReceiver
{

    [SerializeField]
    private List<MaterialMaterialPair> materials;

    [SerializeField]
    private List<float> floats;

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < materials.Count; i++)
        {
            Add(materials[i], floats[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        materials = new List<MaterialMaterialPair>();
        floats = new List<float>();

        foreach (var pair in this)
        {
            materials.Add(pair.Key);
            floats.Add(pair.Value);
        }

    }
}