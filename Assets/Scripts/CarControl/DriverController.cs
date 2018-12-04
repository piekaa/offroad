using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverController : MonoBehaviour
{
    private Car car;

    public Driver Driver;

    void Awake()
    {
        car = GetComponent<Car>();
    }

    void Update()
    {
        car.Accelerate(Driver.Acceleration());
        car.Brake(Driver.Brake());
    }

}
