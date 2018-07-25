using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PiekaSlider : MonoBehaviour {

	[SerializeField]
	private float minValue = 0;

	[SerializeField]
	private float maxValue = 1;

	private Slider slider;
	private Text text;

	public float Value { get; private set; }

	// Use this for initialization
	void Start () {
		slider = GetComponentInChildren<Slider> ();
		text = GetComponentInChildren<Text> ();
		slider.minValue = minValue;
		slider.maxValue = maxValue;
	}
	
	// Update is called once per frame
	void Update () {

		Value = slider.value;
		text.text = Value.ToString("0.00");

	}
}
