using System.Collections.Generic;
using UnityEngine;

public class LevelDto
{
    public List<SpriteDto> sprites = new List<SpriteDto>();
    public List<SpriteShapeControllerDto> SpriteShapeControllers = new List<SpriteShapeControllerDto>();
    public List<Prefab> Prefabs = new List<Prefab>(); 
}