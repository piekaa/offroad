using UnityEngine;
public class SpritePositions
{
    public SpritePositions(Vector2 topLeft, Vector2 topRight, Vector2 bottomLeft, Vector2 bottomRight, Vector2 center)
    {
        TopLeft = topLeft;
        TopRight = topRight;
        BottomLeft = bottomLeft;
        BottomRight = bottomRight;
        Center = center;
        Width = TopRight.x - TopLeft.x;
        Height = TopRight.y - BottomRight.y;
    }

    public Vector2 TopLeft { get; private set; }
    public Vector2 TopRight { get; private set; }
    public Vector2 BottomLeft { get; private set; }
    public Vector2 BottomRight { get; private set; }
    public Vector2 Center { get; private set; }
    public float Width { get; private set; }
    public float Height { get; private set; }



}