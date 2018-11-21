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

        public ParticleSystem SparksParticlePrefab;

        private ParticleSystem sparksParticle;

        private LinkedList<ParticleSystem> particleSystems = new LinkedList<ParticleSystem>();

        private float lastTime = 0;

        private Rigidbody2D rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
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
            if (Time.time > lastTime + 0.1)
            {
                lastTime = Time.time;
                for (int i = 0; i < col.contactCount; i++)
                {
                    sparksParticle = Instantiate(SparksParticlePrefab);
                    var emission = sparksParticle.emission;
                    emission.rateOverTime = kmPerH * EMISSION_RATE;
                    sparksParticle.transform.localScale = new Vector3(1, kmPerH * SCALE_RATE, 1);
                    sparksParticle.transform.position = new Vector3(col.GetContact(i).point.x, col.GetContact(i).point.y, Z_POSSITION);
                    sparksParticle.transform.rotation = Quaternion.Euler(0, 0, angle);
                    particleSystems.AddLast(sparksParticle);
                }
            }
        }

        void Update()
        {
            particleSystems.RemoveAll(item =>
            {
                if (item.time >= item.main.duration)
                {
                    Destroy(item.gameObject);
                    return true;
                }
                return false;
            });
        }
    }
}