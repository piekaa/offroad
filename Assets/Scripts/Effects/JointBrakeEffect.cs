using Pieka.Utils;
using UnityEngine;

public class JointBreakEffect : MonoBehaviour
{
    public ParticleSystem BrakeParticleSystemPrefab;

    private ParticleSystemContainer particleSystemContainer;

    private SpriteRenderer spriteRenderer;

    private const float Z_POSSITION = 10;

    void Start()
    {
        particleSystemContainer = new ParticleSystemContainer(2, BrakeParticleSystemPrefab);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnJointBreak2D(Joint2D brokenJoint)
    {
        var center = (SpriteUtils.GetWolrdPositions(brokenJoint.attachedRigidbody.GetComponent<SpriteRenderer>()).Center + SpriteUtils.GetWolrdPositions(spriteRenderer).Center) / 2;
        var ps = particleSystemContainer.NextAndPlay();
        ps.transform.position = new Vector3(center.x, center.y, Z_POSSITION);
    }

}