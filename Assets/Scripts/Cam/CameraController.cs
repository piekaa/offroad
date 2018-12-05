using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Resetable
{

    [SerializeField]
    private SpriteRenderer curtain;

    [SerializeField]
    private GameObject[] followed;
    private Rigidbody2D followedRigidbody;

    [SerializeField]
    private Material blurMaterial;

    private float cameraSize = 8;

    private float resetDelta = 0;

    private int delay = 30;

    private float cameraTranslationDivider = 8;


    private InterpolatedSum sumX;
    private InterpolatedSum sumY;
    private InterpolatedSum velocitySum;

    private BlurEffect blurEffect;

    void Awake()
    {
        followedRigidbody = followed[0].GetComponent<Rigidbody2D>();

        var avgPos = averageFollowedPosition();

        Camera.main.transform.position = new Vector3(avgPos.x, avgPos.y);
        curtain.transform.position = new Vector3(avgPos.x, avgPos.y, 1);
        blurEffect = Camera.main.gameObject.AddComponent<BlurEffect>();
        blurEffect.Material = blurMaterial;
        ResetSums();
    }

    public override void Reset()
    {
        resetDelta = -0.03f;
        curtain.color = new Color(curtain.color.r, curtain.color.b, curtain.color.g, 1);
        ResetSums();
    }

    void Update()
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

        var avgPos = averageFollowedPosition();

        Camera.main.transform.position = new Vector3(avgPos.x + sumX.Sum, avgPos.y + sumY.Sum);
        curtain.transform.position = new Vector3(avgPos.x, avgPos.y, 1);
        Camera.main.orthographicSize = cameraSize + velocitySum.Sum;
    }

    private Vector2 averageFollowedPosition()
    {
        float followedAverageX = 0;
        float followedAverageY = 0;
        for (int i = 0; i < followed.Length; i++)
        {
            followedAverageX += followed[i].transform.position.x;
            followedAverageY += followed[i].transform.position.y;
        }
        return new Vector2(followedAverageX / followed.Length, followedAverageY / followed.Length);
    }

    private void ResetSums()
    {
        sumX = new InterpolatedSum(delay);
        sumY = new InterpolatedSum(delay);
        velocitySum = new InterpolatedSum(delay);
    }

    public void TurnOnBlur()
    {
        blurEffect.TurnOn();
    }

    public void TurnOffBlur()
    {
        blurEffect.TurnOff();
    }
}