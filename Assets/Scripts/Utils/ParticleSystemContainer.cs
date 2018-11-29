using UnityEngine;
using System.Collections.Generic;

namespace Pieka.Utils
{
    public class ParticleSystemContainer
    {
        private Queue<ParticleSystem> particleSystems;

        public ParticleSystemContainer(int size, ParticleSystem particleSystemPrefab)
        {
            particleSystems = new Queue<ParticleSystem>(size);
            for (int i = 0; i < size; i++)
            {
                var ps = GameObject.Instantiate(particleSystemPrefab, Consts.NOWHERE, Quaternion.identity);
                particleSystems.Enqueue(ps);
            }
        }

        public ParticleSystem Next()
        {
            var ps = particleSystems.Dequeue();
            particleSystems.Enqueue(ps);
            return ps;
        }

        public ParticleSystem NextAndPlay()
        {
            var ps = Next();
            ps.Play();
            return ps;
        }
    }
}
