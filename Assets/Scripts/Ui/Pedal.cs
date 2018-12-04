using UnityEngine;
using UnityEngine.EventSystems;

public enum Direction
{
    up, right, down, left
}

public class Pedal : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private bool RightRotation = false;

    [SerializeField]
    private EventPicker Event;

    int pedalPositionX;
    int pedalPositionY;

    int pedalWidth;
    int pedalHeight;

    [SerializeField]
    private Direction Direction;

    private bool pushed;
    public float Value { get; private set; }

    private RunFloat onIsPressed;

    private RectTransform rectTransform;

    private float xRotationMultiplier = 40;

    private float yRotationMultiplier = -7;

    public void Awake()
    {
        pushed = false;

        pedalHeight = (int)((RectTransform)transform).rect.height;
        pedalWidth = (int)((RectTransform)transform).rect.width;

        pedalPositionY = (int)transform.position.y - pedalHeight / 2;
        pedalPositionX = (int)transform.position.x - pedalWidth / 2;

        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pushed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pushed = false;
        Value = 0;
        rotateAndbroadcast();
    }

    public void RegisterOnIsPressed(RunFloat onIsPressed)
    {
        this.onIsPressed += onIsPressed;
    }

    public void UnregisterOnIsPressed(RunFloat onIsPressed)
    {
        this.onIsPressed -= onIsPressed;
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

            rotateAndbroadcast();

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
        Value = 0;
        rotateAndbroadcast();
        gameObject.SetActive(enabled);
    }

    private void rotateAndbroadcast()
    {
        rotate();
        if (onIsPressed != null)
        {
            onIsPressed(Value);
        }
        if (Event != null)
        {
            SEventSystem.FireEvent(Event.Event, new PMEventArgs(Value));
        }
    }

    private void rotate()
    {
        var sign = RightRotation ? -1 : 1;
        rectTransform.rotation = Quaternion.Euler(Value * xRotationMultiplier, Value * yRotationMultiplier * sign, 0);
    }
}
