using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpeedMeasure : MonoBehaviour
{
    [SerializeField]
    private FloatValue speed;
    private Drive drive;

    void Awake()
    {
        drive = GetComponent<Drive>();
    }

    void Update()
    {
        speed.Value = drive.FrontWheelKmPerH;
    }
}
