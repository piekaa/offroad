namespace Pieka.Car
{
    public interface ICar
    {
        void SetFrontSuspensionFrequency(float frequency);

        void SetFrontDampingRatio(float dampingRatio);

        void SetRearSuspensionFrequency(float frequency);

        void SetRearDampingRatio(float dampingRatio);

        void SetFrontSuspensionHeight(float height);

        void SetRearSuspensionHeight(float height);

        void SetFrontRearDriveRatio(float ratio);

        void Accelerate(float throttle);

        void Brake(float throttle);

        bool ToggleReverse();

        CarInfo GetCarInfo();
    }
}