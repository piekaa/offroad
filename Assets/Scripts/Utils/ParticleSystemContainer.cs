using UnityEngine;
using System.Collections.Generic;


public class ParticleSystemContainer
{
    private Queue<ParticleSystem> particleSystems;

    private ParticleSystem particleSystemPrefab;

    private int size;

    public ParticleSystemContainer(ParticleSystem particleSystemPrefab, int size)
    {
        particleSystems = new Queue<ParticleSystem>(size);
        this.particleSystemPrefab = particleSystemPrefab;
        this.size = size;
    }

    public ParticleSystem Next()
    {
        ParticleSystem ps;
        if (particleSystems.Count < size)
        {
            ps = GameObject.Instantiate(particleSystemPrefab, Consts.NOWHERE, Quaternion.identity);
        }
        else
        {
            ps = particleSystems.Dequeue();
        }
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
