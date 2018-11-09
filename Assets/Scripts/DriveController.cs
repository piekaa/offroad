using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveController : Resetable
{
    [SerializeField]
    private Car car;

    void Awake()
    {
        SetTarget(car.gameObject);
    }
}
