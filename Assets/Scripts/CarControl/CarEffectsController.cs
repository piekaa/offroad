using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEffectsController : PiekaBehaviour
{
    public ParticleSystem SparksParticlePrefab;
    public ParticleSystem BrakeParticleSystemPrefab;

    public Car Car;

    public CarBurnDetector carBurnDetector;

    public PiekaMaterialEffectTable WheelFloorEffectTable;

    void Start()
    {
        var sparkables = Car.GetSparkables();
        foreach (var sparkable in sparkables)
        {
            var collisionSparks = sparkable.gameObject.AddComponent<CollisionSparks>();
            collisionSparks.SparksParticlePrefab = SparksParticlePrefab;
        }

        var brakeables = Car.GetBrakeables();
        foreach (var brakeable in brakeables)
        {
            var jointBreakEffect = brakeable.gameObject.AddComponent<JointBreakEffect>();
            jointBreakEffect.BrakeParticleSystemPrefab = BrakeParticleSystemPrefab;
        }
    }

    [OnEvent(EventNames.WHEEL_BURN)]
    private void OnBurn(string id, PMEventArgs args)
    {
        var burnInfo = (BurnInfo)args.Custom;
        var gameObject = burnInfo.OtherGameObject;
        var wheelMaterial = burnInfo.WheelMaterial;
        var otherObjectWithMaterial = gameObject.GetComponent<ObjectWithMaterial>();
        if (otherObjectWithMaterial != null)
        {
            var effect = WheelFloorEffectTable.GetEffect(wheelMaterial, otherObjectWithMaterial.PiekaMaterial);
            EffectData effectData = new EffectData("burnInfo", burnInfo);
            effectData.Position = burnInfo.Point;
            effect.Play(effectData);
        }
    }
}