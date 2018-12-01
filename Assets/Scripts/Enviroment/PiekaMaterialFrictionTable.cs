using UnityEngine;

[CreateAssetMenu(menuName = "Pieka/MaterialFrictionTable")]
public class PiekaMAterialFrictionTable : ScriptableObject
{
    public MaterialMaterialFloatDictionary dictionary = new MaterialMaterialFloatDictionary();

    public float defaultFriction;

    public float GetEffect(PiekaMaterial material1, PiekaMaterial material2)
    {
        var key = new MaterialMaterialPair(material1, material2);
        if (!dictionary.ContainsKey(key))
        {
            return defaultFriction;
        }
        return dictionary[key];
    }
}