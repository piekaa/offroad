using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reseter : MonoBehaviour
{

    [SerializeField]
    private EventPicker Event;

    private Resetable[] resetables;

    void Start()
    {
        resetables = GameObject.FindObjectsOfType<Resetable>();
    }

    public void Reset()
    {
        foreach (var resetable in resetables)
        {
            resetable.Reset();
        }

        if (Event != null)
        {
            SEventSystem.FireEvent(Event.Event);
        }

    }

}
