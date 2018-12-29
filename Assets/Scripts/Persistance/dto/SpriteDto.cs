using System;
using UnityEditor;
using UnityEngine;

public class SpriteDto : DtoWithTransform
{
    public string SpriteAssetPath;
    public string MaterialAssetPath;
    public Vector4Dto Color;
    public int OrderInLayer;
    public Type Collider2DType;
    public string PiekaMaterialAsset;
    public SpriteDrawMode DrawMode;
    public Vector2Dto Size;
    public  SpriteTileMode TileMode;

    public SpriteDto(SpriteRenderer spriteRenderer)
    {
        SetTransform(spriteRenderer.transform);
        SpriteAssetPath = AssetDatabase.GetAssetPath(spriteRenderer.sprite);
        MaterialAssetPath = AssetDatabase.GetAssetPath(spriteRenderer.sharedMaterial);
        Color = new Vector4Dto(spriteRenderer.color);
        OrderInLayer = spriteRenderer.sortingOrder;
        DrawMode = spriteRenderer.drawMode;
        Size = new Vector2Dto(spriteRenderer.size);
        TileMode = spriteRenderer.tileMode;

        var collider = spriteRenderer.GetComponent<Collider2D>();
        if (collider != null)
        {
            Collider2DType = collider.GetType();
        }

        var objectWithMaterial = spriteRenderer.GetComponent<ObjectWithMaterial>();
        if (objectWithMaterial != null)
        {
            PiekaMaterialAsset = AssetDatabase.GetAssetPath(objectWithMaterial.PiekaMaterial);
        }
    }

    public SpriteDto()
    {
    }
}