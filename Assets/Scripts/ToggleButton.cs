﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <returns>state after toggle</returns>
public delegate bool OnToggle();

public class ToggleButton : MonoBehaviour, IPointerClickHandler
{
    private Image image;
    private OnToggle onToggle;
    void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (onToggle != null)
        {
            var state = onToggle();
            setColor(state);
        }
    }

    public void SetOnToggle(OnToggle onToggle)
    {
        this.onToggle = onToggle;
    }

    private void setColor(bool state)
    {
        if (state)
        {
            image.color = Color.green;
        }
        else
        {
            image.color = Color.white;
        }
    }
}