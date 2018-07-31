using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaks : MonoBehaviour {

	[SerializeField]
	private Pedal breakPedal;

	[SerializeField]
	private Drive drive;

	public void FixedUpdate() 
	{
		drive.BreakFront (breakPedal.Value);
		drive.BreakRear (breakPedal.Value);
	}
}
