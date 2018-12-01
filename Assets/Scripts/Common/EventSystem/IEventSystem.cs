public interface IEventSystem
{
    void FireEvent(string id);
    void FireEvent(string id, PMEventArgs args);
    void Register(string id, PMEvent eventMethod);
    void Unregister(string id, PMEvent eventMethod);

    void Register(string id, PMEventLite eventMethod);
    void Unregister(string id, PMEventLite eventMethod);

}