using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {

	public Rigidbody2D RB { get; private set; }
	public WheelJoint2D Joint { get; private set; }

	// Use this for initialization
	void Start () {
		RB = GetComponent<Rigidbody2D> ();

		var joints = GetComponentsInParent<WheelJoint2D> ();

		if (joints.Length != 2)
		{
			Debug.Log ("Car has wrong number of WheelJoints2D: " + joints.Length);
		}	

		foreach (var joint in joints)
		{
			if (joint.connectedBody == RB)
			{
				Joint = joint;	
			}
		}

		if (Joint == null)
		{
			Debug.Log ("Failed to connect WheelJoint2D");
		}


	}

}
