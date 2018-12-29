using UnityEngine;

public class CarEffectsController : PiekaBehaviour
{
    public ParticleSystem SparksParticlePrefab;
    public ParticleSystem BrakeParticleSystemPrefab;

    public CarHolder CarHolder;
    private Car car;

    public PiekaMaterialEffectTable WheelFloorEffectTable;

    [OnEvent(EventNames.LEVEL_INSTANTIATED)]
    private void OnLevelInstantiate()
    {
        car = CarHolder.Car;
        var sparkables = car.GetSparkables();
        foreach (var sparkable in sparkables)
        {
            var collisionSparks = sparkable.gameObject.AddComponent<CollisionSparks>();
            collisionSparks.SparksParticlePrefab = SparksParticlePrefab;
        }

        var brakeables = car.GetBrakeables();
        foreach (var brakeable in brakeables)
        {
            var jointBreakEffect = brakeable.gameObject.AddComponent<JointBreakEffect>();
            jointBreakEffect.BrakeParticleSystemPrefab = BrakeParticleSystemPrefab;
        }
    }

    [OnEvent(EventNames.WHEEL_BURN)]
    private void OnBurn(string id, PMEventArgs args)
    {
        var burnInfo = (BurnInfo) args.Custom;
        var otherGameObject = burnInfo.OtherGameObject;
        var wheelMaterial = burnInfo.WheelMaterial;
        var otherObjectWithMaterial = otherGameObject.GetComponent<ObjectWithMaterial>();
        if (otherObjectWithMaterial != null)
        {
            var effect = WheelFloorEffectTable.GetEffect(wheelMaterial, otherObjectWithMaterial.PiekaMaterial);
            EffectData effectData = new EffectData("burnInfo", burnInfo);
            effectData.Position = burnInfo.Point;
            effect.Play(effectData);
        }
    }
}