using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Engine : MonoBehaviour
{
    public float RPM { get; private set; }
    public float Torque { get; private set; }
    public float Drag { set; private get; }

    public float Throttle { get; set; }

    private bool clutchEngaged;

    [SerializeField]
    private float horsePower = 300f;

    public Drive Drive;

    void Start()
    {
        Torque = 0.1f;
        Drag = 10;
    }

    void FixedUpdate()
    {
        float currentExpPower = Throttle * horsePower / 25;
        Drive.Accelerate(currentExpPower);
    }
}