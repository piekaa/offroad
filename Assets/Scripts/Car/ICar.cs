using Pieka.Effects;
using UnityEngine;

namespace Pieka.Car
{

    public delegate void OnBurn(BurnInfo burnInfo);

    public interface ICar
    {
        void SetFrontSuspensionFrequency(float frequency);

        void SetFrontDampingRatio(float dampingRatio);

        void SetRearSuspensionFrequency(float frequency);

        void SetRearDampingRatio(float dampingRatio);

        void SetFrontSuspensionHeight(float height);

        void SetRearSuspensionHeight(float height);

        void SetFrontRearDriveRatio(float ratio);

        void SetFrontRearBrakeRatio(float ratio);

        void Accelerate(float throttle);

        void Brake(float throttle);

        bool ToggleReverse();

        CarInfo GetCarInfo();

        Sparkable[] GetSparkables();

        bool IsInAir();

        /// <summary>
        /// Rotating counterclockwise. 0 = Car standing on perfect horisontal floor
        /// </summary>
        /// <returns></returns>
        float GetAngle();

        int WheelsOnFloorCount();

        void RegisterOnBurn(OnBurn onBurn);

        void UnregisterOnBurn(OnBurn onBurn);

        GameObject[] GetBrakeables();
    }
}