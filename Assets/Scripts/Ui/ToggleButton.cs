using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public delegate void OnToggle();

public class ToggleButton : PiekaBehaviour, IPointerClickHandler
{
    public bool BackToInitialOnReset;

    public bool InitialState;

    [SerializeField]
    private EventPicker OnEvent;

    [SerializeField]
    private EventPicker OffEvent;

    private Image image;
    private OnToggle onToggle;

    private bool state;
    void Start()
    {
        state = InitialState;
        image = GetComponent<Image>();
        setColor(state);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        toggle();
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

    [OnEvent(EventNames.RESET)]
    private void onReset()
    {
        if (BackToInitialOnReset && state != InitialState)
        {
            toggle();
        }
    }

    private void toggle()
    {
        state = !state;
        if (onToggle != null)
        {
            onToggle();
        }
        if (state && OnEvent != null)
        {
            SEventSystem.FireEvent(OnEvent.Event);
        }
        if (!state && OffEvent != null)
        {
            SEventSystem.FireEvent(OffEvent.Event);
        }
        setColor(state);
    }
}