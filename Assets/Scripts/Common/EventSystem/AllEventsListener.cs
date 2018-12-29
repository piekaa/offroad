using System;
using UnityEngine;

public class AllEventsListener : MonoBehaviour
{

    IEventSystem eventSystem = EventSystemFactory.GetEventSystem();

    public bool PrintEvents;


    void OnEnable()
    {
        eventSystem.Register("", OnEvent);
    }

    void OnDisable()
    {
        eventSystem.Unregister("", OnEvent);
    }


    void OnEvent(string id, PMEventArgs args)
    {
        if (PrintEvents)
        {
            print("All events listener: " + id);
        }
    }


}

