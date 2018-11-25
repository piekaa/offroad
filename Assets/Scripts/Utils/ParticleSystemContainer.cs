using UnityEngine;
using System.Collections.Generic;
using Pieka.Utils;

public class ParticleSystemContainer
{
    private Queue<ParticleSystem> particleSystems;

    private ParticleSystem particleSystemPrefab;

    public ParticleSystemContainer(int size, ParticleSystem particleSystemPrefab)
    {
        this.particleSystemPrefab = particleSystemPrefab;
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