using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Reverse : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Engine engine;
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        setColor(engine.ToggleReverse());
    }

    private void setColor(bool reverse)
    {
        if (reverse)
        {
            image.color = Color.green;
        }
        else
        {
            image.color = Color.white;
        }
    }

}
