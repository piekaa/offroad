using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode]
public class ObjectWithMaterial : MonoBehaviour
{
    public PiekaMaterial PiekaMaterial;

    void OnEnable()
    {
        Apply();
    }

    public void Apply()
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

        var attachedCollider = GetComponent<Collider2D>();
        if (attachedCollider == null)
        {
            Debug.Log("Collider is not attched to object with collider!");
        }
        else
        {
            attachedCollider.sharedMaterial = PiekaMaterial.PhysicsMaterial2D;
        }
    }
}