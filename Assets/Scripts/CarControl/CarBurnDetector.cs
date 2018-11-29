using System.Collections.Generic;
using Pieka.Car;
using Pieka.Utils;
using UnityEngine;

[CreateAssetMenu(menuName = "Pieka/CarBurnDetector")]
public class CarBurnDetector : ScriptableObject
{

    public float AcceptableSpeedDifference = 10;
    public float BurnSpeedRange = 25;

    private ContactPoint2D[] burnContacts = new ContactPoint2D[10];

    /// <param name="car"></param>
    /// <param name="resultArray">Array to which result will be written</param>
    /// <returns>Number of burn points</returns>
    public int BurnStateInfo(PiekaCar car, BurnInfo[] resultArray)
    {
        var velocityInKmPerH = CalculateUtils.UnitsPerSecondToKmPerH(car.middlePartRigidbody.velocity.magnitude);
        int i = handleBurn(car.FrontWheelCollider, car.FrontWheelSpriteRenderer, BurnInfo.WhichWheelEnum.FRONT, car.FrontWheel.AngularVelocity < 0 ? BurnInfo.DirectionEnum.LEFT : BurnInfo.DirectionEnum.RIGHT, car.Drive.FrontWheelKmPerH, velocityInKmPerH, resultArray, 0);
        return handleBurn(car.RearWheelCollider, car.RearWheelSpriteRenderer, BurnInfo.WhichWheelEnum.REAR, car.RearWheel.AngularVelocity < 0 ? BurnInfo.DirectionEnum.LEFT : BurnInfo.DirectionEnum.RIGHT, car.Drive.RearWheelKmPerH, velocityInKmPerH, resultArray, i);
    }

    /// <returns>current index</returns>
    private int handleBurn(Collider2D wheelCollider, SpriteRenderer spriteRenderer, BurnInfo.WhichWheelEnum whichWheel, BurnInfo.DirectionEnum direction, float wheelKmPerH, float velocityInKmPerH, BurnInfo[] resultArray, int index)
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
                    var gameObject = burnContacts[i].collider.gameObject;
                    var point = burnContacts[i].point;
                    var power = Mathf.Clamp((wheelSpeed - carSpeedPlusAcceptableDifference) / BurnSpeedRange, 0, 1);
                    var burnInfo = new BurnInfo(whichWheel, direction, point, power, gameObject);
                    resultArray[index++] = burnInfo;
                }
            }
        }
        return index;
    }
}