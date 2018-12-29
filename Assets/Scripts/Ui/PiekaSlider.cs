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

public class PiekaSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public SliderTextType SliderTextType;

    [SerializeField]
    private float min = 0;

    [SerializeField]
    private float max = 1;

    [SerializeField]
    private float init = 0.5f;

    private Text text;

    public float Value { get { return val; } set { val = value; setPointerPositionAndText(); } }

    private float val;

    private Image image;

    private float pointerStartX;
    private float startValue;

    private float sensivity = 1 / 300.0f;

    private RunFloat onSlide;

    private Image pointerImage;

    private bool pushed;

    void Awake()
    {
        val = init;
        image = GetComponent<Image>();
        pointerImage = transform.GetChild(0).GetComponent<Image>();
        text = GetComponentInChildren<Text>();
        setPointerPositionAndText();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerStartX = eventData.position.x;
        startValue = val;
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
            val = startValue + (max - min) * normalizedShift;
            val = Mathf.Clamp(val, min, max);
            setPointerPositionAndText();
            if (onSlide != null)
            {

                onSlide(val);
            }
        }
    }

    public void RegisterOnSlide(RunFloat onSlide)
    {
        this.onSlide += onSlide;
    }

    private void setPointerPositionAndText()
    {
        float percent = (val - min) / (max - min);
        var width = image.GetComponent<RectTransform>().rect.width - pointerImage.GetComponent<RectTransform>().rect.width;
        pointerImage.GetComponent<RectTransform>().localPosition = new Vector3(width * percent - width / 2, 0, 0);

        switch (SliderTextType)
        {
            case SliderTextType.PERCENT:
                text.text = (100 * percent).ToString("0.00") + "%";
                break;
            case SliderTextType.VALUE:
                text.text = val.ToString("0.00");
                break;
            default:
                text.text = "";
                break;
        }
    }
}