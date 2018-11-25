using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pieka.Utils;

namespace Pieka.Effects
{
    public class CollisionSparks : MonoBehaviour
    {
        private const int EMISSION_RATE = 15;

        private const float SCALE_RATE = 0.05f;

        private const float Z_POSSITION = 10;

        private const float SPARKS_INSTANTIATE_PERIOD = 0.1f;

        public ParticleSystem SparksParticlePrefab;

        private ParticleSystem sparksParticle;

        private ParticleSystemContainer particleSystemContainer;

        private float lastTime = 0;

        private Rigidbody2D rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            particleSystemContainer = new ParticleSystemContainer(10, SparksParticlePrefab);
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            instantiateSparks(col);
        }

        void OnCollisionStay2D(Collision2D col)
        {
            instantiateSparks(col);
        }

        private void instantiateSparks(Collision2D col)
        {
            if (col.gameObject.layer != Consts.FloorLayer)
            {
                return;
            }
            var kmPerH = CalculateUtils.UnitsPerSecondToKmPerH(rb.velocity.magnitude);
            if (kmPerH < 5)
            {
                return;
            }
            float angle = CalculateUtils.Vector2ToAngle(rb.velocity);
            if (Time.time > lastTime + SPARKS_INSTANTIATE_PERIOD)
            {
                lastTime = Time.time;
                for (int i = 0; i < col.contactCount; i++)
                {
                    sparksParticle = particleSystemContainer.NextAndPlay();
                    var emission = sparksParticle.emission;
                    emission.rateOverTime = kmPerH * EMISSION_RATE;
                    sparksParticle.transform.localScale = new Vector3(1, kmPerH * SCALE_RATE, 1);
                    sparksParticle.transform.position = new Vector3(col.GetContact(i).point.x, col.GetContact(i).point.y, Z_POSSITION);
                    sparksParticle.transform.rotation = Quaternion.Euler(0, 0, angle);
                }
            }
        }
    }
}