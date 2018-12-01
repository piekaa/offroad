using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CarBonusController : Resetable, ICarBonusController
{

    private const int MILLIS_TO_ACCEPT_FLIP = 1000;

    private Run onFlip;

    [SerializeField]
    private PiekaCar car;
    public ICar Car;

    bool wasInAirLastTime;

    bool angleWasNear180;

    new void Start()
    {
        Car = car;
    }

    public void RegisterOnFlip(Run onFlip)
    {
        this.onFlip += onFlip;
    }
    public void UnregisterOnFlip(Run onFlip)
    {
        this.onFlip -= onFlip;
    }

    void FixedUpdate()
    {
        var wheelsOnFloorCount = Car.WheelsOnFloorCount();
        if (wasInAirLastTime && !Car.IsInAir())
        {
            if (angleWasNear180 && wheelsOnFloorCount >= 1)
            {
                if (onFlip != null)
                {
                    onFlip();
                }
            }
        }
        if (Car.IsInAir())
        {
            var angle = Car.GetAngle();

            if (angle >= 150 && angle <= 210)
            {
                angleWasNear180 = true;
            }
        }
        else
        {
            angleWasNear180 = false;
        }
        wasInAirLastTime = Car.IsInAir();
    }

    public override void Reset()
    {
        angleWasNear180 = false;
    }
}