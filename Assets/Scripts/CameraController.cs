using System.Collections;
using System.Collections.Generic;
using Pieka.Car;
using Pieka.Utils;
using UnityEngine;

public class CameraController : Resetable
{

    [SerializeField]
    private SpriteRenderer curtain;

    [SerializeField]
    private GameObject followed;
    private Rigidbody2D followedRigidbody;

    private float cameraSize = 8;

    private float resetDelta = 0;

    private int delay = 30;

    private float cameraTranslationDivider = 8;


    private InterpolatedSum sumX;
    private InterpolatedSum sumY;
    private InterpolatedSum velocitySum;

    void Awake()
    {
        followedRigidbody = followed.GetComponent<Rigidbody2D>();
        Camera.main.transform.position = new Vector3(followed.transform.position.x, followed.transform.position.y);
        curtain.transform.position = new Vector3(followed.transform.position.x, followed.transform.position.y, 1);
        ResetSums();
    }

    public override void Reset()
    {
        resetDelta = -0.03f;
        curtain.color = new Color(curtain.color.r, curtain.color.b, curtain.color.g, 1);
        ResetSums();
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
        var translation = followedRigidbody.velocity / cameraTranslationDivider;

        sumX.Add(translation.x);
        sumY.Add(translation.y);
        velocitySum.Add(translation.magnitude);

        Camera.main.transform.position = new Vector3(followed.transform.position.x + sumX.Sum, followed.transform.position.y + sumY.Sum);
        curtain.transform.position = new Vector3(followed.transform.position.x, followed.transform.position.y, 1);
        Camera.main.orthographicSize = cameraSize + velocitySum.Sum;
    }

    private void ResetSums()
    {
        sumX = new InterpolatedSum(delay);
        sumY = new InterpolatedSum(delay);
        velocitySum = new InterpolatedSum(delay);
    }

}
