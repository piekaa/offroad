namespace Pieka.CarControl
{
    public interface ICarDriveController
    {
        IMeter SpeedMeter { get; set; }

        IPedal AccelerationPedal { get; set; }

        IPedal BrakePedal { get; set; }
    }
}