using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PiekaButton : MonoBehaviour, IPointerClickHandler
{
    private Run onClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null)
        {
            onClick();
        }
    }

    public void RegisterOnClick(Run onClick)
    {
        this.onClick += onClick;
    }

    public void UnregisterOnClick(Run onClick)
    {
        this.onClick -= onClick;
    }
}
