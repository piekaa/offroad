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

            var burnEffect = gameObject.AddComponent<BurnEffect>();
            burnEffect.BurnParticlePrefab = this.BurnParticlePrefab;
            Car.RegisterOnBurn(burnEffect.OnBurn);
        }
    }
}