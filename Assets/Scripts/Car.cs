using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Resetable
{

    [SerializeField]
    private Wheel frontWheel;

    [SerializeField]
    private Wheel rearWheel;

    private Body body;
	private Vector3 bodyInitialPosition;

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

    //TODO: remove from Car component
    [SerializeField]
    private PiekaSlider frontWheelSlider;

    //TODO: remove from Car component
    [SerializeField]
    private PiekaSlider rearWheelSlider;

    //TODO: remove from Car component
    [SerializeField]
    private PiekaSlider frontWheelDampSlider;

    //TODO: remove from Car component
    [SerializeField]
    private PiekaSlider rearWheelDampSlider;

    void Start()
    {
		body = GetComponentInChildren<Body>();
		bodyInitialPosition = body.transform.position;
    }

    void Update()
    {
        var suspension = frontWheel.Joint.suspension;
        suspension.frequency = frontWheelSlider.Value;
        suspension.dampingRatio = frontWheelDampSlider.Value;
        frontWheel.Joint.suspension = suspension;

        suspension = rearWheel.Joint.suspension;
        suspension.frequency = rearWheelSlider.Value;
        suspension.dampingRatio = rearWheelDampSlider.Value;
        rearWheel.Joint.suspension = suspension;
    }

	public override void Reset() 
	{
		body.transform.position = bodyInitialPosition;		
		body.transform.rotation = Quaternion.identity;
	}
}
