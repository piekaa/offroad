using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pieka/CarBurnDetector")]
public class CarBurnDetector : CarStateDetector
{

    public float AcceptableSpeedDifference = 10;
    public float BurnSpeedRange = 25;

    protected override IEnumerator detectCoroutine(Car car)
    {
        ContactPoint2D[] burnContacts = new ContactPoint2D[10];
        var frontWheelMaterial = car.FrontWheel.PiekaMaterial;
        var rearWheelMaterial = car.FrontWheel.PiekaMaterial;
        for (; ; )
        {
            detect(car, burnContacts, frontWheelMaterial, rearWheelMaterial);
            yield return null;
        }
    }

    private void detect(Car car, ContactPoint2D[] burnContacts, PiekaMaterial frontWheelMaterial, PiekaMaterial rearWheelMaterial)
    {
        var velocityInKmPerH = CalculateUtils.UnitsPerSecondToKmPerH(car.middlePartRigidbody.velocity.magnitude);
        handleBurn(car.FrontWheelCollider, car.FrontWheelSpriteRenderer, BurnInfo.WhichWheelEnum.FRONT, car.FrontWheel.AngularVelocity < 0 ? BurnInfo.DirectionEnum.LEFT : BurnInfo.DirectionEnum.RIGHT, car.Drive.FrontWheelKmPerH, velocityInKmPerH, burnContacts, frontWheelMaterial);
        handleBurn(car.RearWheelCollider, car.RearWheelSpriteRenderer, BurnInfo.WhichWheelEnum.REAR, car.RearWheel.AngularVelocity < 0 ? BurnInfo.DirectionEnum.LEFT : BurnInfo.DirectionEnum.RIGHT, car.Drive.RearWheelKmPerH, velocityInKmPerH, burnContacts, rearWheelMaterial);
    }

    private void handleBurn(Collider2D wheelCollider, SpriteRenderer spriteRenderer, BurnInfo.WhichWheelEnum whichWheel, BurnInfo.DirectionEnum direction, float wheelKmPerH, float velocityInKmPerH, ContactPoint2D[] burnContacts, PiekaMaterial piekaMaterial)
    {
        if (wheelCollider.IsTouchingLayers(Consts.FloorLayerMask))
        {
            var carSpeedPlusAcceptableDifference = velocityInKmPerH + AcceptableSpeedDifference;
            var wheelSpeed = wheelKmPerH;
            if (wheelSpeed > carSpeedPlusAcceptableDifference)
            {
                var filter = new ContactFilter2D();
                filter.layerMask = Consts.FloorLayerMask;
                var numberOfContacts = wheelCollider.GetContacts(filter, burnContacts);
                for (int i = 0; i < numberOfContacts; i++)
                {
                    SEventSystem.FireEvent(EventNames.TEST);
                    var gameObject = burnContacts[i].collider.gameObject;
                    var point = burnContacts[i].point;
                    var power = Mathf.Clamp((wheelSpeed - carSpeedPlusAcceptableDifference) / BurnSpeedRange, 0, 1);
                    var burnInfo = new BurnInfo(whichWheel, direction, point, power, gameObject, piekaMaterial);
                    SEventSystem.FireEvent(EventNames.WHEEL_BURN, new PMEventArgs(burnInfo));
                }
            }
        }
    }
}