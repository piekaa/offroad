using System;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : IEventSystem
{
    private Dictionary<string, PMEvent> events = new Dictionary<string, PMEvent>();
    private Dictionary<string, PMEventLite> eventsLite = new Dictionary<string, PMEventLite>();
    private PMEvent allEvents;
    private PMEventLite allEventsLite;

    public void FireEvent(string id)
    {
        FireEvent(id, null);
    }

    public void FireEvent(string id, PMEventArgs args)
    {
        if (events.ContainsKey(id))
        {
            if (events[id] != null)
                events[id](id, args);
        }
        if (allEvents != null)
            allEvents(id, args);


        if (eventsLite.ContainsKey(id))
        {
            if (eventsLite[id] != null)
                eventsLite[id]();
        }
        if (allEventsLite != null)
            allEventsLite();

    }

    public void Register(string id, PMEvent eventMethod)
    {
        if (id != "")
        {
            if (!events.ContainsKey(id))
                events[id] = eventMethod;
            else
                events[id] += eventMethod;
        }
        else
            allEvents += eventMethod;
    }

    public void Unregister(string id, PMEvent eventMethod)
    {
        if (id != "")
        {
            events[id] -= eventMethod;
        }
        else
            allEvents -= eventMethod;
    }



    public void Register(string id, PMEventLite eventMethod)
    {
        if (id != "")
        {
            if (!eventsLite.ContainsKey(id))
                eventsLite[id] = eventMethod;
            else
                eventsLite[id] += eventMethod;
        }
        else
            allEventsLite += eventMethod;
    }

    public void Unregister(string id, PMEventLite eventMethod)
    {
        if (id != "")
        {
            eventsLite[id] -= eventMethod;
        }
        else
            allEventsLite -= eventMethod;
    }
}

