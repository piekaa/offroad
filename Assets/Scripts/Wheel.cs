using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour, IWheel
{

    private Rigidbody2D Rigidbody;
    public WheelJoint2D Joint { get; private set; }

    //todo maybe take that from sprite
    public float DiameterInMeters { get; set; }

    [SerializeField]
    private float diameterInMeters;

    public void Awake()
    {
        DiameterInMeters = diameterInMeters;
    }

    public void AddTorque(float torque)
    {
        Rigidbody.AddTorque(torque);
    }

    public float AngularDrag
    {
        get
        {
            return Rigidbody.angularDrag;
        }

        set
        {
            Rigidbody.angularDrag = value;
        }
    }

    public float AngularVelocity
    {
        get
        {
            return Rigidbody.angularVelocity;
        }

        set
        {
            Rigidbody.angularVelocity = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();

        var joints = GetComponentsInParent<WheelJoint2D>();

        if (joints.Length != 2)
        {
            Debug.Log("Car has wrong number of WheelJoints2D: " + joints.Length);
        }

        foreach (var joint in joints)
        {
            if (joint.connectedBody == Rigidbody)
            {
                Joint = joint;
            }
        }

        if (Joint == null)
        {
            Debug.Log("Failed to connect WheelJoint2D");
        }


    }

}
