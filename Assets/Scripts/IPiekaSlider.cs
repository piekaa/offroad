public delegate void OnSlide(float value);

public interface IPiekaSlider
{
    float Value { get; }
    void setOnSlide(OnSlide onSlide);
}