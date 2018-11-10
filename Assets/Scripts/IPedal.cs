public delegate void OnIsPressed(float value);

public interface IPedal
{
    //todo Add On Pressed
    float Value { get; }

    void RegisterOnIsPressed(OnIsPressed onIsPressed);
}