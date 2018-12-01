using System;

public delegate void PMEvent(string id, PMEventArgs args);
public delegate void PMEventLite();

public class SEventSystem
{
    private static IEventSystem eventSystem = EventSystemFactory.GetEventSystem();

    public static void FireEvent(string id)
    {
        eventSystem.FireEvent(id);
    }

    public static void FireEvent(string id, PMEventArgs args)
    {
        eventSystem.FireEvent(id, args);
    }

    public static void Register(string id, PMEvent eventMethod)
    {
        eventSystem.Register(id, eventMethod);
    }

    public static void Unregister(string id, PMEvent eventMethod)
    {
        eventSystem.Unregister(id, eventMethod);
    }

    public static void Register(string id, PMEventLite eventMethod)
    {
        eventSystem.Register(id, eventMethod);
    }

    public static void Unregister(string id, PMEventLite eventMethod)
    {
        eventSystem.Unregister(id, eventMethod);
    }
}

