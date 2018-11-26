using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pieka.Effects;
using Pieka.Car;

namespace Pieka.CarControl
{
    public class CarEffectsController : MonoBehaviour, ICarEffectsController
    {
        public ParticleSystem SparksParticlePrefab;

        public ParticleSystem BurnParticlePrefab;

        public ParticleSystem BrakeParticleSystemPrefab;

        [SerializeField]
        private PiekaCar car;
        public ICar Car;

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

            var burnEffect = gameObject.AddComponent<BurnEffect>();
            burnEffect.BurnParticlePrefab = this.BurnParticlePrefab;
            Car.RegisterOnBurn(burnEffect.OnBurn);
        }
    }
}