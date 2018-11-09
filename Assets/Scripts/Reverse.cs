using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Returns true if gear is reverse after toggle
public delegate bool OnReverseToggle();

public class Reverse : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private IDrive engine;
    private Image image;
    private OnReverseToggle onReverseToggle;
    void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (onReverseToggle != null)
        {
            var reverse = onReverseToggle();
            setColor(reverse);
        }
    }

    public void SetOnReverseToggle(OnReverseToggle onReverseToggle)
    {
        this.onReverseToggle = onReverseToggle;
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
