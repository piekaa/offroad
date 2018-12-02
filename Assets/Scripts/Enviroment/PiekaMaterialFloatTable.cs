using UnityEngine;

[CreateAssetMenu(menuName = "Pieka/MaterialFloatTable")]
public class PiekaMaterialFloatTable : ScriptableObject
{
    [SerializeField]
    public MaterialMaterialFloatDictionary dictionary = new MaterialMaterialFloatDictionary();

    public float Default;

    public float GetFloat(PiekaMaterial material1, PiekaMaterial material2)
    {
        var key = new MaterialMaterialPair(material1, material2);
        if (!dictionary.ContainsKey(key))
        {
            return Default;
        }
        return dictionary[key];
    }
}