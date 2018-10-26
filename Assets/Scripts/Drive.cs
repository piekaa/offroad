using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : OrderedScript
{

	[SerializeField]
	public Car Car;

	public float FrontBreakPower = 50;
	public float RearBreakPower = 50;

	private int registered;

	private bool brakeingFront;
	private bool brakeingRear;

	/// <summary>
	/// Accelerates the front. Invoke only in FixedUpdate
	/// </summary>
	/// <param name="power">Torque</param>
	public void AccelerateFront (float power)
	{
		Car.FrontWheel.RB.AddTorque (-power);
	}

	/// <summary>
	/// Accelerates the front. Invoke only in FixedUpdate
	/// </summary>
	/// <param name="power">Torque</param>
	public void AccelerateRear (float power)
	{
		Car.RearWheel.RB.AddTorque (-power);
	}

	/// <summary>
	/// Brakes the front. Invoke only in FixedUpdate
	/// </summary>
	/// <param name="power">0-1</param>
	public void BrakeFront (float power)
	{
		if (power > 0)
		{
			brakeingFront = true;
		}
		doBrake (Car.FrontWheel, power * FrontBreakPower);
	}

	/// <summary>
	/// Brakes the front. Invoke only in FixedUpdate
	/// </summary>
	/// <param name="power">0-1</param>
	public void BrakeRear (float power)
	{
		if (power > 0)
		{
			brakeingRear = true;
		}
		doBrake (Car.RearWheel, power * RearBreakPower);
	}

	private void doBrake (Wheel wheel, float power)
	{
		wheel.RB.angularDrag = power;

		var motor = wheel.Joint.motor;
		float sign = Mathf.Sign (wheel.RB.angularVelocity);
		float newSpeed = Mathf.Abs (wheel.RB.angularVelocity) - power;

		newSpeed = Mathf.Max (newSpeed, 0);

		motor.motorSpeed = newSpeed * sign;

		wheel.Joint.useMotor = true;
		wheel.Joint.motor = motor;
		 
	}

	public float FrontWheelSpeed { get { return Car.FrontWheel.RB.angularVelocity; } }

	public float RearWheelSpeed { get { return Car.RearWheel.RB.angularVelocity; } }

	public float FrontWheelRPM { get { return Car.FrontWheel.RB.angularVelocity / 6; } }

	public float RearWheelRPM { get { return Car.RearWheel.RB.angularVelocity / 6; } }


	public override void OrderedFixedUpdate ()
	{ 
		if (!brakeingFront)
		{
			Car.FrontWheel.Joint.useMotor = false;
			Car.FrontWheel.RB.angularDrag = 0;
		}

		if (!brakeingRear)
		{
			Car.RearWheel.Joint.useMotor = false;
			Car.RearWheel.RB.angularDrag = 0;
		}


		brakeingFront = false;
		brakeingRear = false;
	}

	//TODO: Move to other component
	void LateUpdate ()
	{
		Camera.main.transform.rotation = Quaternion.identity;
	}

}
