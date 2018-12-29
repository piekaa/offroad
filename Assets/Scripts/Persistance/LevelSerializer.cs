using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public enum WorldType
{
    NORMAL,
    WINTER
}

public class LevelSerializer : MonoBehaviour
{
    public WorldType WorldType;

    public void Serialize()
    {
        var carSerialized = false;
        var levelDto = new LevelDto();
        for (int i = 0; i < transform.childCount; i++)
        {
            var spriteRenderer = transform.GetChild(i).GetComponent<SpriteRenderer>();
            var spriteShapeController = transform.GetChild(i).GetComponent<SpriteShapeController>();
            var prefab = transform.GetChild(i).GetComponent<ImPrefabSerializeMe>();
            
            if (spriteRenderer != null)
            {
                levelDto.sprites.Add(new SpriteDto(spriteRenderer));
            }

            if (spriteShapeController != null)
            {
                levelDto.SpriteShapeControllers.Add(new SpriteShapeControllerDto(spriteShapeController));
            }

            if (prefab != null)
            {
                if( prefab.GetComponent<Car>() != null)
                {
                    carSerialized = true;
                }
                levelDto.Prefabs.Add(new Prefab(prefab));
            }
        }

        if (carSerialized)
        {
            var level = ScriptableObject.CreateInstance<Level>();
            level.Json = Piekson.ToJson(levelDto);
            AssetDatabase.CreateAsset(level,
                "Assets/Levels/" + WorldType + "/" + SceneManager.GetActiveScene().name + ".asset");
            Debug.Log(Piekson.ToJson(levelDto));
        }
        else
        {
            Debug.Log("Car is missing!");
        }
        
    }

    private void Start()
    {
        SEventSystem.FireEvent(EventNames.LEVEL_INSTANTIATED);
    }
}