using UnityEngine.U2D;
using UnityEngine;

[ExecuteInEditMode]
public class SpriteShapeExntension : MonoBehaviour
{
    public PiekaMaterial PiekaMaterial;

    void OnEnable()
    {
        updateSpriteShapeController();
    }

    void Update()
    {
#if (UNITY_EDITOR)
        updateSpriteShapeController();
#endif
    }

    private void updateSpriteShapeController()
    {
        var spriteShapeController = GetComponent<SpriteShapeController>();
        spriteShapeController.spriteShape = PiekaMaterial.SpriteShape;
        var collider = GetComponent<Collider2D>();
        collider.sharedMaterial = PiekaMaterial.PhysicsMaterial2D;
    }
}