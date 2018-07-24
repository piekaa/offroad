using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

	private WheelJoint2D rightWheelJoint;
	private WheelJoint2D leftWheelJoint;

	[SerializeField]
	private PiekaSlider rightWheelSlider;

	[SerializeField]
	private PiekaSlider leftWheelSlider;

	// Use this for initialization
	void Start () {
		var joints = GetComponentsInChildren<WheelJoint2D> ();
		rightWheelJoint = joints [0];
		leftWheelJoint = joints [1];
	}
	
	// Update is called once per frame
	void Update () {

		var suspension = rightWheelJoint.suspension;
		suspension.frequency = rightWheelSlider.Value;
		rightWheelJoint.suspension = suspension;

		suspension = leftWheelJoint.suspension;
		suspension.frequency = leftWheelSlider.Value;
		leftWheelJoint.suspension = suspension;
	}
}
