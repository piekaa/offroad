using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pieka/MaterialEffectTable")]
public class PiekaMaterialEffectTable : ScriptableObject
{
    [SerializeField]
    public MaterialMaterialEffectDictionary dictionary = new MaterialMaterialEffectDictionary();

    public Effect defaultEffect;

    public Effect GetEffect(PiekaMaterial material1, PiekaMaterial material2)
    {
        var key = new MaterialMaterialPair(material1, material2);
        if (!dictionary.ContainsKey(key))
        {
            return defaultEffect;
        }
        return dictionary[key];
    }
}
