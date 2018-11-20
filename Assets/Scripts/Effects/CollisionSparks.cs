using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pieka.Utils;

namespace Pieka.Effects
{
    public class CollisionSparks : MonoBehaviour
    {
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

            float angle = 0;
            if (rb.velocity.x <= 0)
            {
                angle = Vector2.Angle(new Vector2(0, 1), rb.velocity);
            }
            else
            {
                angle = Vector2.Angle(new Vector2(0, -1), rb.velocity);
            }

            if (Time.time > lastTime + 0.1)
            {
                lastTime = Time.time;
                for (int i = 0; i < col.contactCount; i++)
                {
                    sparksParticle = Instantiate(SparksParticlePrefab);
                    var emission = sparksParticle.emission;
                    emission.rateOverTime = kmPerH * 15;
                    sparksParticle.transform.localScale = new Vector3(1, kmPerH / 20, 1);
                    sparksParticle.transform.position = new Vector3(col.GetContact(i).point.x, col.GetContact(i).point.y, 10);
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