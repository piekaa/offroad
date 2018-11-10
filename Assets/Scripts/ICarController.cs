public interface ICarController
{
    IPiekaSlider FrontWheelSlider { get; set; }

    IPiekaSlider RearWheelSlider { get; set; }

    IPiekaSlider FrontWheelDampSlider { get; set; }

    IPiekaSlider RearWheelDampSlider { get; set; }

    IPiekaSlider FrontRearDriveRatioSlider { get; set; }

    IMeter SpeedMeter { get; set; }

    IPedal AccelerationPedal { get; set; }

    IPedal BrakePedal { get; set; }

}