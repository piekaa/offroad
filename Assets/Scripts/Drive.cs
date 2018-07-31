using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Drive : MonoBehaviour {

	[SerializeField]
	public Car Car;

	public float FrontAccelerationPower = 5;
	public float RearAccelerationPower = 5;

	public float FrontBreakPower = 50;
	public float RearBreakPower = 50;


	/// <summary>
	/// Accelerates the front. Invoke only in FixedUpdate
	/// </summary>
	/// <param name="power">0-1</param>
	public void AccelerateFront(float power) 
	{
		Car.FrontWheel.RB.AddTorque (-power*FrontAccelerationPower);
	}

	/// <summary>
	/// Accelerates the front. Invoke only in FixedUpdate
	/// </summary>
	/// <param name="power">0-1</param>
	public void AccelerateRear(float power) 
	{
		Car.RearWheel.RB.AddTorque (-power*FrontAccelerationPower);
	}

	/// <summary>
	/// Accelerates the front. Invoke only in FixedUpdate
	/// </summary>
	/// <param name="power">0-1</param>
	public void BreakFront(float power) 
	{
		doBreak (Car.FrontWheel, power * FrontBreakPower);
	}

	/// <summary>
	/// Accelerates the front. Invoke only in FixedUpdate
	/// </summary>
	/// <param name="power">0-1</param>
	public void BreakRear(float power) 
	{
		doBreak (Car.RearWheel, power * RearBreakPower);
	}


	private void doBreak(Wheel wheel, float power)
	{
		wheel.RB.angularDrag = power;

		if (power > 0)
		{
			var motor = wheel.Joint.motor;
			float sign = Mathf.Sign (wheel.RB.angularVelocity);
			float newSpeed = Mathf.Abs (wheel.RB.angularVelocity) - power;

			newSpeed = Mathf.Max (newSpeed, 0);

			motor.motorSpeed = newSpeed * sign;

			wheel.Joint.useMotor = true;
			wheel.Joint.motor = motor;

		} else
		{
			wheel.Joint.useMotor = false;
		}
	}
		

	//TODO: Move to other component
	void LateUpdate() {
		Camera.main.transform.rotation = Quaternion.identity;
	}

}
