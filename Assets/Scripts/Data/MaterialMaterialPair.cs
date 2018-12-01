using System;
using System.Collections.Generic;

[Serializable]
public class MaterialMaterialPair
{
    public PiekaMaterial Key;
    public PiekaMaterial Value;

    public MaterialMaterialPair(PiekaMaterial key, PiekaMaterial value)
    {
        Key = key;
        Value = value;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (MaterialMaterialPair)obj;

        return Key.Equals(other.Key) && Value.Equals(other.Value);
    }

    public override int GetHashCode()
    {
        return Key.GetHashCode() + Value.GetHashCode();
    }
}