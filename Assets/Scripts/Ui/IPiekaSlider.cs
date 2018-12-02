public interface IPiekaSlider
{
    float Value { get; }
    void RegisterOnSlide(RunFloat onSlide);
}
