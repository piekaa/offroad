using Pieka.Utils;

namespace Pieka.Ui
{

    public interface IPiekaSlider
    {
        float Value { get; }
        void RegisterOnSlide(RunFloat onSlide);
    }
}