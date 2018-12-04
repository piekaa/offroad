using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Driver : ScriptablePieka
{
    public abstract float Acceleration();

    public abstract float Brake();
}
