using Pieka.Ui;

namespace Pieka.CarControl
{
    public interface ICarSettingsController
    {
        IPiekaSlider FrontWheelSlider { get; set; }

        IPiekaSlider RearWheelSlider { get; set; }

        IPiekaSlider FrontWheelDampSlider { get; set; }

        IPiekaSlider RearWheelDampSlider { get; set; }

        IPiekaSlider FrontRearDriveRatioSlider { get; set; }

        IPiekaSlider RearSuspensionHeightSlider { get; set; }

        IPiekaSlider FrontSuspensionHeightSlider { get; set; }
    }
}