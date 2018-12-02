using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour, IWheel
{
    private Rigidbody2D rb;

    public float DiameterInMeters { get; private set; }

    public void Awake()
    {
        DiameterInMeters = SpriteUtils.GetWolrdPositions(GetComponent<SpriteRenderer>()).Width * Consts.MetersPerWroldUnit;
        rb = GetComponent<Rigidbody2D>();
        piekaMaterial = GetComponent<ObjectWithMaterial>().PiekaMaterial;
    }

    public void AddTorque(float torque)
    {
        rb.AddTorque(torque);
    }

    private PiekaMaterial piekaMaterial;

    public PiekaMaterial PiekaMaterial
    {
        get
        {
            return piekaMaterial;
        }
    }

    public float AngularDrag
    {
        get
        {
            return rb.angularDrag;
        }

        set
        {
            rb.angularDrag = value;
        }
    }

    public float AngularVelocity
    {
        get
        {
            return rb.angularVelocity;
        }

        set
        {
            rb.angularVelocity = value;
        }
    }
}