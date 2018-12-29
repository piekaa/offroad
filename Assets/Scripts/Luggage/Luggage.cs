using UnityEngine;

public class Luggage : Resetable
{
    public LuggageHolder LuggageHolder;

    private Rigidbody2D[] rigidbodies;

    void Awake()
    {
        LuggageHolder.Luggage = this;
        rigidbodies = GetComponentsInChildren<Rigidbody2D>();
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