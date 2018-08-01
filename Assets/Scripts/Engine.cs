using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Temp implementation
public class Engine : OrderedScript {

	[SerializeField]
	private Pedal accelerationPedal;

	[SerializeField]
	private Drive drive;

	public override void OrderedFixedUpdate() 
	{
		drive.AccelerateFront (accelerationPedal.Value);
	}

}
