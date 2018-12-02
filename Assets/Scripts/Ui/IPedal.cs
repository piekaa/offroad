public interface IPedal
{
    float Value { get; }

    void RegisterOnIsPressed(RunFloat onIsPressed);
    void Enable();
    void Disable();

}
