public delegate void OnIsPressed(float value);

public interface IPedal
{
    float Value { get; }

    void RegisterOnIsPressed(OnIsPressed onIsPressed);
}