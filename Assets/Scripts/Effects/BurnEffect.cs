using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pieka/BurnEffect")]
public class BurnEffect : Effect
{
    /// <summary>
    /// every FrequencyMillis play new particle system
    /// </summary>
    public float FrequencyMillis = 40;

    public ParticleSystem BurnParticlePrefab;

    private const float Y_ROTATION = 90;
    private const float Z_ROTATION = 0;
    private const float X_ROTATION = 270;

    private float lastTime;

    private ParticleSystemContainer particleSystemContainer;


    public void OnEnable()
    {
        lastTime = 0;
        particleSystemContainer = new ParticleSystemContainer(BurnParticlePrefab, 10);
    }

    public override void Play(EffectData effectData)
    {
        BurnInfo burnInfo = (BurnInfo)effectData.Map["burnInfo"];
        if (Time.time > lastTime + FrequencyMillis / 1000f)
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