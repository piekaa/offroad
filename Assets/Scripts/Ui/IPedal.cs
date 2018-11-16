namespace Pieka.Ui
{
    public delegate void OnIsPressed(float value);

    public interface IPedal
    {
        float Value { get; }

        void RegisterOnIsPressed(OnIsPressed onIsPressed);
        void Enable();
        void Disable();

    }
}