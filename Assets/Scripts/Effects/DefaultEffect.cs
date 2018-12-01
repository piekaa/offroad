using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pieka/DefaultEffect")]
public class DefaultEffect : Effect
{
    public ParticleSystem DefaultParticlePrefab;

    private float lastTime;

    private ParticleSystemContainer particleSystemContainer;


    public void OnEnable()
    {
        particleSystemContainer = new ParticleSystemContainer(DefaultParticlePrefab, 10);
    }

    public override void Play(EffectData effectData)
    {
        var ps = particleSystemContainer.NextAndPlay();
        ps.transform.position = new Vector3(effectData.Position.x, effectData.Position.y, Z_POSSITION);
        lastTime = Time.time;
    }
}