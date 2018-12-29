using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteShapeControllerDto : DtoWithTransform
{
    public SplineDto SplineDto;
    public int SplineDetail;
    public string SpriteShapeAsset;
    public string PiekaMaterialAsset;
    public Type Collider2DType;
    public int ColliderDetail;
    public float ColliderOffset;
    public ColliderCornerType ColliderCornerType;

    public SpriteShapeControllerDto()
    {
    }

    public SpriteShapeControllerDto(SpriteShapeController spriteShapeController)
    {
        SetTransform(spriteShapeController.transform);
        SplineDto = new SplineDto(spriteShapeController.spline);
        SplineDetail = spriteShapeController.splineDetail;
        SpriteShapeAsset = AssetDatabase.GetAssetPath(spriteShapeController.spriteShape);
        var objectWithMaterial = spriteShapeController.GetComponent<ObjectWithMaterial>();
        if (objectWithMaterial != null)
        {
            PiekaMaterialAsset = AssetDatabase.GetAssetPath(objectWithMaterial.PiekaMaterial);
        }

        var collider2D = spriteShapeController.GetComponent<Collider2D>();
        if (collider2D != null)
        {
            Collider2DType = collider2D.GetType();
        }
        ColliderDetail = spriteShapeController.colliderDetail;
        ColliderOffset = spriteShapeController.colliderOffset;
        ColliderCornerType = spriteShapeController.colliderCornerType;
    }
}