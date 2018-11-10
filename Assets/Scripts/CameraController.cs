using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Resetable
{

    [SerializeField]
    private SpriteRenderer curtain;

    [SerializeField]
    private GameObject followed;

    float resetDelta = 0;

    void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(followed.transform.position.x, followed.transform.position.y);
        curtain.transform.position = new Vector3(followed.transform.position.x, followed.transform.position.y, 1);
    }

    public override void Reset()
    {
        resetDelta = -0.03f;
        curtain.color = new Color(curtain.color.r, curtain.color.b, curtain.color.g, 1);
    }

    void FixedUpdate()
    {
        if (resetDelta != 0)
        {
            curtain.color = new Color(curtain.color.r, curtain.color.b, curtain.color.g, curtain.color.a + resetDelta);
            if (curtain.color.a <= 0)
            {
                resetDelta = 0;
            }
        }
    }

}
