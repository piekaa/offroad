using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaggageController : Resetable
{
    [SerializeField]
    private GameObject laggage;

    private Rigidbody2D[] rigidbodies;

    void Awake()
    {
        SetTarget(laggage);
        rigidbodies = laggage.GetComponentsInChildren<Rigidbody2D>();
        foreach (var rb in rigidbodies)
        {
            rb.gravityScale = 0;
        }
    }

    public override void Reset()
    {
        base.Reset();
        foreach (var rb in rigidbodies)
        {
            rb.gravityScale = 0;
        }
    }

    public void Release()
    {
        foreach (var rb in rigidbodies)
        {
            rb.gravityScale = 1;
        }
    }

    public void Fly()
    {
        foreach (var rb in rigidbodies)
        {
            rb.gravityScale = -2;
        }
    }
}