using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Temp implementation
public class Engine : OrderedScript {

	public float RPM { get; private set; }

	public float Torque { get; private set; }

	public float Drag { set; private get; }

	/// <summary>
	/// 0-1
	/// </summary>
	private float throttle;

	private bool clutchEngaged;

	[SerializeField]
	private int cylinders = 4;

	[SerializeField]
	private float explosionPower = 10;

	void Start(){

		Torque = 0.1f;
		Drag = 10;
		RPM = 2000;
		throttle = 0;
	}

	[SerializeField]
	private Pedal accelerationPedal;

	[SerializeField]
	private Pedal clutchPedal;

	[SerializeField]
	private Drive drive;

	float ratio = 0.01f;

	public override void OrderedFixedUpdate() 
	{
		clutchEngaged = false;
		if (clutchPedal.Value > 0.8)
		{
			clutchEngaged = true;
		}

		float WRPM = Mathf.Abs (drive.FrontWheelRPM);

		if (clutchEngaged)
		{
			RPM = WRPM / ratio;
		} else
		{
			
			throttle = accelerationPedal.Value;
			if (RPM < 1000)
			{
				throttle = Mathf.Max (0.3f, accelerationPedal.Value);
			}

			float currentExpPower = 120000 / (cylinders * RPM) * throttle * explosionPower;

			RPM += currentExpPower;

			RPM -= Drag;

			float t = (RPM * Torque * clutchPedal.Value) * ratio;

			float diff = 1 - (WRPM / (RPM * ratio));

			Debug.Log("Torque:" + t); 
			Debug.Log("Diff:" + diff); 

			drive.AccelerateFront (t * diff);

		}

		Debug.Log ("RPM: " + RPM);
		Debug.Log ("FW RPM: " + WRPM);

		//drive.AccelerateRear (accelerationPedal.Value);
		//drive.AccelerateFront (accelerationPedal.Value);
	}




}
