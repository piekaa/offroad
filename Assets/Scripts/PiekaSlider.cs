using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PiekaSlider : MonoBehaviour
{

    [SerializeField]
    private float min = 0;

    [SerializeField]
    private float max = 1;

    [SerializeField]
    private float init = 0.5f;

    private Slider slider;
    private Text text;

    public float Value { get; private set; }

    // Use this for initialization
    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        text = GetComponentInChildren<Text>();
        slider.minValue = min;
        slider.maxValue = max;
		slider.value = init;
    }

    // Update is called once per frame
    void Update()
    {
        Value = slider.value;
        text.text = Value.ToString("0.00");

    }
}
