using UnityEngine;
using UnityEngine.EventSystems;

public enum Direction
{
    up, right, down, left
}

public class Pedal : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPedal
{

    int pedalPositionX;
    int pedalPositionY;

    int pedalWidth;
    int pedalHeight;

    [SerializeField]
    private Direction Direction;

    private bool pushed;
    public float Value { get; private set; }

    private OnIsPressed onIsPressed;

    public void Awake()
    {
        pushed = false;

        pedalHeight = (int)((RectTransform)transform).rect.height;
        pedalWidth = (int)((RectTransform)transform).rect.width;

        pedalPositionY = (int)transform.position.y - pedalHeight / 2;
        pedalPositionX = (int)transform.position.x - pedalWidth / 2;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pushed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pushed = false;
        Value = 0;
        if (onIsPressed != null)
        {
            onIsPressed(Value);
        }
    }

    public void RegisterOnIsPressed(OnIsPressed onIsPressed)
    {
        this.onIsPressed += onIsPressed;
    }

    public void Update()
    {
        if (pushed && enabled)
        {
            int mx = (int)Input.mousePosition.x;
            int my = (int)Input.mousePosition.y;

            switch (Direction)
            {
                case Direction.up:
                    Value = Mathf.Clamp((my - pedalPositionY) / (float)pedalHeight, 0, 1);
                    break;
                case Direction.right:
                    Value = Mathf.Clamp((mx - pedalPositionX) / (float)pedalWidth, 0, 1);
                    break;
                case Direction.down:
                    Value = Mathf.Clamp((my - pedalPositionY) / (float)pedalHeight, 0, 1);
                    Value = (1 - Value);
                    break;
                case Direction.left:
                    Value = Mathf.Clamp((mx - pedalPositionX) / (float)pedalWidth, 0, 1);
                    Value = 1 - Value;
                    break;
            }
            if (onIsPressed != null)
            {
                onIsPressed(Value);
            }

        }
    }

    public void Enable()
    {
        enabled = true;
        gameObject.SetActive(enabled);
    }

    public void Disable()
    {
        enabled = false;
        if (onIsPressed != null)
        {
            onIsPressed(0);
        }
        gameObject.SetActive(enabled);
    }
}
