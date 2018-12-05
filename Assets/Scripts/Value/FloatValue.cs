using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pieka/Values/Float")]
public class FloatValue : ScriptableObject
{
    public float Value;

    public float InitialValue;

    public bool FallbackToInitialOnEnable;

    void OnEnable()
    {
        if (FallbackToInitialOnEnable)
        {
            Value = InitialValue;
        }
    }
}
