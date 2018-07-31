using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

	[SerializeField]
	private Wheel frontWheel;

	[SerializeField]
	private Wheel rearWheel;

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
	private PiekaSlider rightWheelSlider;

	//TODO: remove from Car component
	[SerializeField]
	private PiekaSlider leftWheelSlider;

	// Update is called once per frame
	void Update () {

		var suspension = frontWheel.Joint.suspension;
		suspension.frequency = rightWheelSlider.Value;
		frontWheel.Joint.suspension = suspension;

		suspension = rearWheel.Joint.suspension;
		suspension.frequency = leftWheelSlider.Value;
		rearWheel.Joint.suspension = suspension;
	}
}
