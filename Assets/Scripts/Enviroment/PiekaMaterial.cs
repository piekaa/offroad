using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


[CreateAssetMenu(menuName = "Pieka/Material")]
public class PiekaMaterial : ScriptableObject
{
    public SpriteShape SpriteShape;

    public PhysicsMaterial2D PhysicsMaterial2D;

    public BurnEffect BurnEffect;
}