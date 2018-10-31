using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PiekaSlider : MonoBehaviour, IPointerDownHandler, IDragHandler
{

    [SerializeField]
    private float min = 0;

    [SerializeField]
    private float max = 1;

    [SerializeField]
    private float init = 0.5f;

    private Slider slider;
    private Text text;

    //todo set private set
    // public float Value { get; private set; }
    public float Value;
    
    private Image image;

    private float pointerStartX;
    private float startValue;

    private float sensivity = 1/300.0f;

    void Start()
    {
        Value = init;
        image = GetComponent<Image>();
        float percent = (Value - min) / (max - min);
        image.color = new Color(percent, 1-percent, 0);
    }
 
    public void OnPointerDown(PointerEventData eventData)
    {
        pointerStartX = eventData.position.x;
        startValue = Value; 
    }

    public void OnDrag(PointerEventData eventData)
    {
        float shiftInPixels = (eventData.position.x - pointerStartX);
        float normalizedShift = Mathf.Clamp( shiftInPixels * sensivity, -1, 1 );
        Value = startValue + (max-min)*normalizedShift;
        Value = Mathf.Clamp(Value, min, max);
        float percent = (Value - min) / (max - min); 
        image.color = new Color(percent, 1-percent, 0);
    }
}
