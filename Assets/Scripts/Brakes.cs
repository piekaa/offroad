using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brakes : OrderedScript {

	//todo move pedal to CarController
	[SerializeField]
	private Pedal brakePedal;

	[SerializeField]
	private Drive drive;

	public override void OrderedFixedUpdate() 
	{
		drive.BrakeFront (brakePedal.Value);
		drive.BrakeRear (brakePedal.Value);
	}
}
