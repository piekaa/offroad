using System.Collections;
using System.Collections.Generic;
using Pieka.Car;
using Pieka.Utils;
using UnityEngine;

[CreateAssetMenu(menuName = "Pieka/BurnEffect")]
public class BurnEffect : Initializable
{
    /// <summary>
    /// every FREQUENCY millis play new particle system
    /// </summary>
    public float FrequencyMillis = 40;

    public ParticleSystem BurnParticlePrefab;

    private const float Z_POSSITION = 10;
    private const float Y_ROTATION = 90;
    private const float Z_ROTATION = 0;
    private const float X_ROTATION = 270;

    private float lastTime;


    private ParticleSystemContainer particleSystemContainer;

    public override void Init()
    {
        particleSystemContainer = new ParticleSystemContainer(10, BurnParticlePrefab);
        lastTime = 0;
    }

    public void Play(BurnInfo burnInfo)
    {

        Debug.Log("Play");

        if (Time.time > lastTime + FrequencyMillis / 1000f)
        {
            Debug.Log("It's time");

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