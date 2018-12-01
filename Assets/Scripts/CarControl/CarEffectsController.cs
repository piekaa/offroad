using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEffectsController : MonoBehaviour, ICarEffectsController
{
    public ParticleSystem SparksParticlePrefab;
    public ParticleSystem BrakeParticleSystemPrefab;

    [SerializeField]
    private PiekaCar car;
    public ICar Car;

    public CarBurnDetector carBurnDetector;

    private BurnInfo[] burnInfos = new BurnInfo[10];

    void Start()
    {
        Car = car;
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

    void Update()
    {
        var count = carBurnDetector.BurnStateInfo(car, burnInfos);
        for (int i = 0; i < count; i++)
        {
            var gameObject = burnInfos[0].GameObject;
            var spriteShapeExtension = gameObject.GetComponent<SpriteShapeExntension>();
            if (spriteShapeExtension != null)
            {
                var effect = spriteShapeExtension.PiekaMaterial.BurnEffect;
                effect.Play(new EffectData("burnInfo", burnInfos[0]));
            }
        }
    }
}