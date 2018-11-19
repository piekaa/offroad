using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pieka.Effects
{
    public class CollisionSparks : MonoBehaviour
    {
        public ParticleSystem SparksParticlePrefab;

        private ParticleSystem sparksParticle;

        void OnCollisionEnter2D(Collision2D col)
        {
            if (sparksParticle == null)
            {
                sparksParticle = Instantiate(SparksParticlePrefab);
                sparksParticle.transform.position = col.GetContact(0).point;
                sparksParticle.transform.parent = transform;
            }
        }

        void OnCollisionExit2D(Collision2D other)
        {
            if (sparksParticle != null)
            {
                Destroy(sparksParticle.gameObject);
            }
        }

    }
}