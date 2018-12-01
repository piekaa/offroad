using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MaterialMaterialEffectDictionary : Dictionary<MaterialMaterialPair, Effect>, ISerializationCallbackReceiver
{

    [SerializeField]
    private List<MaterialMaterialPair> materials;

    [SerializeField]
    private List<Effect> effects;

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < materials.Count; i++)
        {
            Add(materials[i], effects[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        materials = new List<MaterialMaterialPair>();
        effects = new List<Effect>();

        foreach (var pair in this)
        {
            materials.Add(pair.Key);
            effects.Add(pair.Value);
        }

    }
}