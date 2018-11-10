using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum SliderTextType
{
    NONE,
    PERCENT,
    VALUE
}

public class PiekaSlider : MonoBehaviour, IPiekaSlider, IPointerDownHandler, IPointerUpHandler
{

    public SliderTextType SliderTextType;

    [SerializeField]
    private float min = 0;

    [SerializeField]
    private float max = 1;

    [SerializeField]
    private float init = 0.5f;

    private Text text;

    public float Value { get; private set; }

    private Image image;

    private float pointerStartX;
    private float startValue;

    private float sensivity = 1 / 300.0f;

    private OnSlide onSlide;

    private Image pointerImage;

    private bool pushed;

    void Start()
    {
        Value = init;
        image = GetComponent<Image>();
        float percent = (Value - min) / (max - min);
        pointerImage = transform.GetChild(0).GetComponent<Image>();
        text = GetComponentInChildren<Text>();
        setPointerPositionAndText(percent);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerStartX = eventData.position.x;
        startValue = Value;
        pushed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pushed = false;
    }

    void Update()
    {
        if (pushed)
        {
            float shiftInPixels = (Input.mousePosition.x - pointerStartX);
            float normalizedShift = Mathf.Clamp(shiftInPixels * sensivity, -1, 1);
            Value = startValue + (max - min) * normalizedShift;
            Value = Mathf.Clamp(Value, min, max);
            float percent = (Value - min) / (max - min);
            setPointerPositionAndText(percent);
            if (onSlide != null)
            {
                onSlide(Value);
            }
        }
    }

    public void RegisterOnSlide(OnSlide onSlide)
    {
        this.onSlide += onSlide;
    }

    private void setPointerPositionAndText(float percent)
    {
        var width = image.GetComponent<RectTransform>().rect.width - pointerImage.GetComponent<RectTransform>().rect.width;
        pointerImage.GetComponent<RectTransform>().localPosition = new Vector3(width * percent - width / 2, 0, 0);

        switch (SliderTextType)
        {
            case SliderTextType.PERCENT:
                text.text = (100 * percent).ToString("0.00") + "%";
                break;
            case SliderTextType.VALUE:
                text.text = Value.ToString("0.00");
                break;
            default:
                text.text = "";
                break;
        }
    }


}
