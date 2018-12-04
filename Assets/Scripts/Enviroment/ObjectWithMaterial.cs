using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode]
public class ObjectWithMaterial : MonoBehaviour
{
    public PiekaMaterial PiekaMaterial;

    void OnEnable()
    {
        if (PiekaMaterial == null)
        {
            return;
        }

        var spriteShapeController = GetComponent<SpriteShapeController>();
        if (spriteShapeController != null)
        {
            spriteShapeController.spriteShape = PiekaMaterial.SpriteShape;
        }
        var collider = GetComponent<Collider2D>();
        collider.sharedMaterial = PiekaMaterial.PhysicsMaterial2D;
    }
}
