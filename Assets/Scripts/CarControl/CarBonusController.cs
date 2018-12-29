public class CarBonusController : Resetable
{
    private const int MILLIS_TO_ACCEPT_FLIP = 1000;

    private Run onFlip;

    public CarHolder CarHolder;
    
    private Car car;

    bool wasInAirLastTime;

    bool angleWasNear180;


    [OnEvent(EventNames.LEVEL_INSTANTIATED)]
    private void OnLevelInstantiate()
    {
        car = CarHolder.Car;
        enabled = true;
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
        var wheelsOnFloorCount = car.WheelsOnFloorCount();
        if (wasInAirLastTime && !car.IsInAir())
        {
            if (angleWasNear180 && wheelsOnFloorCount >= 1)
            {
                if (onFlip != null)
                {
                    onFlip();
                }
            }
        }

        if (car.IsInAir())
        {
            var angle = car.GetAngle();

            if (angle >= 150 && angle <= 210)
            {
                angleWasNear180 = true;
            }
        }
        else
        {
            angleWasNear180 = false;
        }

        wasInAirLastTime = car.IsInAir();
    }

    public override void Reset()
    {
        angleWasNear180 = false;
    }
}