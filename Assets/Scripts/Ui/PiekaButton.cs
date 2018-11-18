using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PiekaButton : MonoBehaviour, IPiekaButton, IPointerClickHandler
{
    private OnClick onClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null)
        {
            onClick();
        }
    }

    public void RegisterOnClick(OnClick onClick)
    {
        this.onClick += onClick;
    }

    public void UnregisterOnClick(OnClick onClick)
    {
        this.onClick -= onClick;
    }
}
