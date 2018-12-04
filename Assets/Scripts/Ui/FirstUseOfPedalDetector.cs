using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstUseOfPedalDetector : PiekaBehaviour
{

    [SerializeField]
    private EventPicker Event;

    private bool used;

    private Pedal pedal;

    void Awake()
    {
        pedal = GetComponent<Pedal>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        pedal.RegisterOnIsPressed(onPressed);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        pedal.UnregisterOnIsPressed(onPressed);
    }

    private void onPressed(float value)
    {
        if (!used)
        {
            FireEvent(Event.Event);
        }
        used = true;
    }


    [OnEvent(EventNames.RESET)]
    private void OnReset()
    {
        used = false;
    }
}
