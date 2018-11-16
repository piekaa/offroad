namespace Pieka.Ui
{
    public delegate void OnSlide(float value);

    public interface IPiekaSlider
    {
        float Value { get; }
        void RegisterOnSlide(OnSlide onSlide);
    }
}