using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    protected const float Z_POSSITION = 10;

    public abstract void Play(EffectData effectData);
}
