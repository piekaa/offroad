using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


[CreateAssetMenu(menuName = "Pieka/Material")]
public class PiekaMaterial : ScriptableObject
{
    public SpriteShape SpriteShape;

    public Sprite Sprite;

    public PhysicsMaterial2D PhysicsMaterial2D;
}