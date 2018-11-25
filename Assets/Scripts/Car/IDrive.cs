using UnityEngine;

namespace Pieka.Car
{
    interface IDrive
    {

        /// <value>0 means rear wheel drive, 1 means front wheel drive</value>
        float FrontRearDriveRatio { get; set; }


        /// <value>0 means rear wheel only brake, 1 means front wheel only brake</value>
        float FrontRearBrakeRatio { get; set; }

        /// <returns>true if engine is on reverse</returns>
        bool ToggleReverse();

        float FrontWheelRpm { get; }

        float RearWheelRpm { get; }

        float FrontWheelKmPerH { get; }

        float RearWheelKmPerH { get; }

        void SetFrontWheel(IWheel wheel);

        void SetRearWheel(IWheel wheel);

        /// <summary>
        /// Accelerates the front. Invoke only in FixedUpdate
        /// </summary>
        /// <param name="power">Torque</param>
        void Accelerate(float power);

        /// <summary>
        /// Invoke only in FixedUpdate
        /// </summary>
        /// <param name="power">0-1</param>
        void Brake(float power);

        void SetJoints(WheelJoint2D front, WheelJoint2D rear);
    }
}