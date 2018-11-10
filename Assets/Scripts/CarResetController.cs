using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pieka.Car;

public class CarResetController : Resetable
{
    [SerializeField]
    private Car car;
    public ICar Car;

    void Awake()
    {
        Car = car;
        SetTarget(car.gameObject);
    }
}
