using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Temp implementation
public class Engine : MonoBehaviour {

	[SerializeField]
	private Pedal accelerationPedal;

	[SerializeField]
	private Drive drive;

	public void FixedUpdate() 
	{
		drive.AccelerateFront (accelerationPedal.Value);
	}

}
