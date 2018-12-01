using System;

public class EventSystemFactory
{
    private static IEventSystem eventSystem = new EventSystem();

    public static IEventSystem GetEventSystem()
    {
        return eventSystem;
    }
}

