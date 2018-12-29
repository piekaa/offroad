using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class LevelSpawner : MonoBehaviour
{
    public Level Level;

    void Awake()
    {
        var levelData = Piekson.FromJson<LevelDto>(Level.Json);
        spawnSprites(levelData);
        spawnSpriteShapes(levelData);
        var carWasSpawned = spawnPrefabs(levelData);
        if (carWasSpawned)
        {
            SEventSystem.FireEvent(EventNames.LEVEL_INSTANTIATED);
        }
        else
        {
            Debug.Log("Car was not instantiated!");
        }
    }

    private void spawnSprites(LevelDto levelData)
    {
        foreach (var spriteDto in levelData.sprites)
        {
            var newGameObject = new GameObject();
            newGameObject.transform.parent = transform;
            newGameObject.transform.name = spriteDto.Name;
            var newSpriteRenderer = newGameObject.AddComponent<SpriteRenderer>();
            newGameObject.transform.position = spriteDto.Position.ToVector3();
            newGameObject.transform.rotation = spriteDto.Rotation.ToQuaterion();
            newGameObject.transform.localScale = spriteDto.Scale.ToVector3();
            newSpriteRenderer.color = spriteDto.Color.ToVector4();
            newSpriteRenderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(spriteDto.SpriteAssetPath);
            newSpriteRenderer.sharedMaterial = AssetDatabase.LoadAssetAtPath<Material>(spriteDto.MaterialAssetPath);
            newSpriteRenderer.sortingOrder = spriteDto.OrderInLayer;
            newSpriteRenderer.drawMode = spriteDto.DrawMode;
            newSpriteRenderer.tileMode = spriteDto.TileMode;
            newSpriteRenderer.size = spriteDto.Size.ToVector2();


            if (spriteDto.Collider2DType != null)
            {
                newSpriteRenderer.gameObject.AddComponent(spriteDto.Collider2DType);
            }

            if (spriteDto.PiekaMaterialAsset != null)
            {
                var objectWithMaterial = newSpriteRenderer.gameObject.AddComponent<ObjectWithMaterial>();
                objectWithMaterial.PiekaMaterial =
                    AssetDatabase.LoadAssetAtPath<PiekaMaterial>(spriteDto.PiekaMaterialAsset);
                objectWithMaterial.Apply();
            }
        }
    }


    private void spawnSpriteShapes(LevelDto levelData)
    {
        foreach (var spriteShapeControllerDto in levelData.SpriteShapeControllers)
        {
            var newGameObject = new GameObject();
            newGameObject.transform.parent = transform;
            newGameObject.transform.name = spriteShapeControllerDto.Name;
            var newSpriteShapeController = newGameObject.AddComponent<SpriteShapeController>();
            newGameObject.transform.position = spriteShapeControllerDto.Position.ToVector3();
            newGameObject.transform.rotation = spriteShapeControllerDto.Rotation.ToQuaterion();
            newGameObject.transform.localScale = spriteShapeControllerDto.Scale.ToVector3();
            newSpriteShapeController.splineDetail = spriteShapeControllerDto.SplineDetail;
            newSpriteShapeController.spline.isOpenEnded = spriteShapeControllerDto.SplineDto.openEnded;
            newSpriteShapeController.colliderDetail = spriteShapeControllerDto.ColliderDetail;
            newSpriteShapeController.colliderOffset = spriteShapeControllerDto.ColliderOffset;
            newSpriteShapeController.colliderCornerType = spriteShapeControllerDto.ColliderCornerType;

            var index = 0;
            foreach (var point in spriteShapeControllerDto.SplineDto.Points)
            {
                newSpriteShapeController.spline.InsertPointAt(index, point.Position.ToVector3());
                newSpriteShapeController.spline.SetTangentMode(index, point.TangentMode);
                newSpriteShapeController.spline.SetRightTangent(index, point.RightTangent.ToVector3());
                newSpriteShapeController.spline.SetLeftTangent(index, point.LeftTangent.ToVector3());
                index++;
            }

            if (spriteShapeControllerDto.Collider2DType != null)
            {
                newSpriteShapeController.gameObject.AddComponent(spriteShapeControllerDto.Collider2DType);
            }

            if (spriteShapeControllerDto.PiekaMaterialAsset != null)
            {
                var objectWithMaterial = newSpriteShapeController.gameObject.AddComponent<ObjectWithMaterial>();
                objectWithMaterial.PiekaMaterial =
                    AssetDatabase.LoadAssetAtPath<PiekaMaterial>(spriteShapeControllerDto.PiekaMaterialAsset);
                objectWithMaterial.Apply();
            }
            else
            {
                var spriteShape = AssetDatabase.LoadAssetAtPath<SpriteShape>(spriteShapeControllerDto.SpriteShapeAsset);
                newSpriteShapeController.spriteShape = spriteShape;
            }

            newSpriteShapeController.BakeCollider();
        }
    }

    /// <summary>
    /// spawns prefabs
    /// </summary>
    /// <param name="levelData"></param>
    /// <returns>True if car was spawned</returns>
    private bool spawnPrefabs(LevelDto levelData)
    {
        var carWasSpawned = false;
        foreach (var prefab in levelData.Prefabs)
        {   
            var prefabAsset = AssetDatabase.LoadAssetAtPath<GameObject>(prefab.PrefabPath);
            var newPrefab = Instantiate(prefabAsset, prefab.Position.ToVector3(), prefab.Rotation.ToQuaterion());
            newPrefab.transform.name = prefab.Name;
            newPrefab.transform.localScale = prefab.Scale.ToVector3();
            newPrefab.transform.parent = transform;

            if (newPrefab.GetComponent<Car>() != null)
            {
                carWasSpawned = true;
            }
        }

        return carWasSpawned;
    }
}