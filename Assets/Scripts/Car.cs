using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public IDrive Drive;

    [SerializeField]
    private Wheel frontWheel;
    [SerializeField]
    private Wheel rearWheel;

    void Awake()
    {
        Drive = GetComponentInChildren<Drive>();
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
