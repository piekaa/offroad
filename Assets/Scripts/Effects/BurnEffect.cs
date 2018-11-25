using System.Collections;
using System.Collections.Generic;
using Pieka.Car;
using Pieka.Utils;
using UnityEngine;

namespace Pieka.Effects
{
    public class BurnEffect : MonoBehaviour
    {

        /// <summary>
        /// every FREQUENCY millis play new particle system
        /// </summary>
        private const float FREQUENCY = 40 / 1000f;

        public ParticleSystem BurnParticlePrefab;

        private const float Z_POSSITION = 5;

        private const float Y_ROTATION = 90;

        private const float Z_ROTATION = 0;

        private const float X_ROTATION = 270;

        private float lastTime;


        private ParticleSystemContainer particleSystemContainer;


        void Start()
        {
            particleSystemContainer = new ParticleSystemContainer(15, BurnParticlePrefab);
        }

        public void OnBurn(BurnInfo burnInfo)
        {
            if (Time.time > lastTime + FREQUENCY)
            {
                var ps = particleSystemContainer.NextAndPlay();
                var emission = ps.emission;
                emission.rateOverTimeMultiplier = 100 * burnInfo.Power;
                var main = ps.main;
                main.startSizeMultiplier = burnInfo.Power;
                ps.transform.position = new Vector3(burnInfo.Point.x, burnInfo.Point.y, Z_POSSITION);
                lastTime = Time.time;
            }
        }
    }
}