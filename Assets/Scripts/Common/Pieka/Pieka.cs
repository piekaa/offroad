using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class PairEventLiteString
{
    public string Name;
    public PMEventLite Event;
    public PairEventLiteString(string name, PMEventLite @event)
    {
        this.Name = name;
        this.Event = @event;
    }

}

public class PairEventString
{
    public string Name;
    public PMEvent Event;
    public PairEventString(string name, PMEvent @event)
    {
        this.Name = name;
        this.Event = @event;
    }
}

public abstract class PiekaBehaviour : MonoBehaviour
{
    protected HashSet<string> allowedStates = new HashSet<string>();
    protected List<PairEventString> events = new List<PairEventString>();
    protected List<PairEventLiteString> eventsLite = new List<PairEventLiteString>();

    public virtual void OnEnable()
    {
        MethodInfo[] methods = GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
        foreach (MethodInfo m in methods)
        {
            System.Attribute[] attributes = (System.Attribute[])m.GetCustomAttributes(true);
            foreach (System.Attribute attribute in attributes)
            {
                if (attribute is OnEvent)
                {
                    OnEvent onEventMethod = (OnEvent)attribute;
                    if (m.GetParameters().Length == 2)
                    {
                        PMEvent del = (PMEvent)Delegate.CreateDelegate(typeof(PMEvent), this, m);
                        events.Add(new PairEventString(onEventMethod.Name, del));
                    }
                    else
                    {
                        PMEventLite del = (PMEventLite)Delegate.CreateDelegate(typeof(PMEventLite), this, m);
                        eventsLite.Add(new PairEventLiteString(onEventMethod.Name, del));
                    }
                }
            }
        }
        RegisterAll();
    }

    public virtual void OnDisable()
    {
        UnregisterAll();
    }

    protected void UnregisterAll()
    {
        foreach (PairEventString e in events)
        {
            SEventSystem.Unregister(e.Name, e.Event);
        }

        foreach (PairEventLiteString e in eventsLite)
        {
            SEventSystem.Unregister(e.Name, e.Event);
        }
    }

    protected void RegisterAll()
    {
        foreach (PairEventString e in events)
        {
            SEventSystem.Register(e.Name, e.Event);
        }

        foreach (PairEventLiteString e in eventsLite)
        {
            SEventSystem.Register(e.Name, e.Event);
        }
    }

    protected void FireEvent(string id)
    {
        SEventSystem.FireEvent(id);
    }

    protected void FireEvent(string id, PMEventArgs args)
    {
        SEventSystem.FireEvent(id, args);
    }
}


public abstract class ScriptablePieka : ScriptableObject
{
    protected HashSet<string> allowedStates = new HashSet<string>();
    protected List<PairEventString> events = new List<PairEventString>();
    protected List<PairEventLiteString> eventsLite = new List<PairEventLiteString>();

    public virtual void OnEnable()
    {
        MethodInfo[] methods = GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
        foreach (MethodInfo m in methods)
        {
            System.Attribute[] attributes = (System.Attribute[])m.GetCustomAttributes(true);
            foreach (System.Attribute attribute in attributes)
            {
                if (attribute is OnEvent)
                {
                    OnEvent onEventMethod = (OnEvent)attribute;
                    if (m.GetParameters().Length == 2)
                    {
                        PMEvent del = (PMEvent)Delegate.CreateDelegate(typeof(PMEvent), this, m);
                        events.Add(new PairEventString(onEventMethod.Name, del));
                    }
                    else
                    {
                        PMEventLite del = (PMEventLite)Delegate.CreateDelegate(typeof(PMEventLite), this, m);
                        eventsLite.Add(new PairEventLiteString(onEventMethod.Name, del));
                    }
                }
            }
        }
        RegisterAll();
    }

    public virtual void OnDisable()
    {
        UnregisterAll();
    }

    protected void UnregisterAll()
    {
        foreach (PairEventString e in events)
        {
            SEventSystem.Unregister(e.Name, e.Event);
        }

        foreach (PairEventLiteString e in eventsLite)
        {
            SEventSystem.Unregister(e.Name, e.Event);
        }
    }

    protected void RegisterAll()
    {
        foreach (PairEventString e in events)
        {
            SEventSystem.Register(e.Name, e.Event);
        }

        foreach (PairEventLiteString e in eventsLite)
        {
            SEventSystem.Register(e.Name, e.Event);
        }
    }

    protected void FireEvent(string id)
    {
        SEventSystem.FireEvent(id);
    }

    protected void FireEvent(string id, PMEventArgs args)
    {
        SEventSystem.FireEvent(id, args);
    }
}
