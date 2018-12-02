using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meter : MonoBehaviour, IMeter
{
    public float Value { get; set; }

    [SerializeField]
    private float min;

    [SerializeField]
    private float max;

    [SerializeField]
    private float interval;

    private float startAngle = 130;

    private float endAngle = -130;

    private RectTransform pointerRectTransform;

    // Use this for initialization
    void Start()
    {
        pointerRectTransform = GetComponentInChildren<Pointer>().GetComponent<RectTransform>();
        var number = GetComponentInChildren<Number>();
        if (interval <= 0)
        {
            Debug.Log("Interval is <= 0 ");
            return;
        }
        var numberOfSteps = ((max - min) / interval + 1);
        var angleStep = (endAngle - startAngle) / (numberOfSteps - 1);
        for (int i = 0; i < numberOfSteps; i++)
        {
            var n = Instantiate(number, gameObject.transform);
            n.GetComponentInChildren<Text>().text = (min + interval * i) + "";
            var angle = startAngle + angleStep * i;
            n.GetComponent<RectTransform>().localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            n.GetComponentInChildren<Text>().GetComponent<RectTransform>().localRotation = Quaternion.Euler(new Vector3(0, 0, -angle));
        }
    }

    void Update()
    {
        float percentage = (Value - min) / (float)(max - min);
        pointerRectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, (endAngle - startAngle) * percentage + startAngle));

    }
}