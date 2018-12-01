using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelEffect : MonoBehaviour
{
    public Sprite[] sprites;
    private Rigidbody2D rb;

    private Wheel wheel;

    void Awake()
    {
        wheel = GetComponent<Wheel>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var rpm = Mathf.Abs(rb.angularVelocity / 6);
        float kmPerh = CalculateUtils.WheelRpmToKmPerHour(rpm, wheel.DiameterInMeters);
        kmPerh -= 10;
        kmPerh = kmPerh < 0 ? 0 : kmPerh;
        var block = new MaterialPropertyBlock();
        int index = (int)(kmPerh / 10) + 1;
        index = index > 4 ? 4 : index;
        while (kmPerh >= 10)
        {
            kmPerh -= 10;
        }
        block.SetTexture("_ATex", sprites[index - 1].texture);
        block.SetTexture("_BTex", sprites[index].texture);
        block.SetFloat("_Trans", kmPerh / 10);
        GetComponent<SpriteRenderer>().SetPropertyBlock(block);
    }
}