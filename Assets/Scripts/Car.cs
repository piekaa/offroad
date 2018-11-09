using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour, ICar
{
    public IDrive Drive { get; set; }
    
    public IEngine Engine { get; set; }

    [SerializeField]
    private Wheel frontWheel;
    [SerializeField]
    private Wheel rearWheel;

    void Awake()
    {
        Drive = GetComponentInChildren<Drive>();
        Engine = GetComponentInChildren<Engine>();
        Drive.SetFrontWheel(frontWheel);
        Drive.SetRearWheel(rearWheel);
    }

    //todo return Interface
    public Wheel FrontWheel
    {
        get
        {
            return frontWheel;
        }

    }
    public Wheel RearWheel
    {
        get
        {
            return rearWheel;
        }
    }
}
