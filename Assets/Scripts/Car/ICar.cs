using Pieka.Effects;
using UnityEngine;

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

        void SetFrontRearBrakeRatio(float ratio);

        void Accelerate(float throttle);

        void Brake(float throttle);

        bool ToggleReverse();

        CarInfo GetCarInfo();

        Vector3 GetVelocity();

        Sparkable[] GetSparkables();

        bool IsInAir();

        /// <summary>
        /// Rotating counterclockwise. 0 = Car standing on perfect horisontal floor
        /// </summary>
        /// <returns></returns>
        float GetAngle();

        int WheelsOnFloorCount();
    }
}