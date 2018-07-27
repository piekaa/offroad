using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour {
 
	private SpriteRenderer spriteRenderer;

	public GameObject Spawnable;
	public float Angle;

	void Start () {
		spriteRenderer = GetComponentInChildren<SpriteRenderer> ();
		spriteRenderer.color = Color.clear;
	}

	public void Spawn() {

		Spawnable.transform.position = spriteRenderer.transform.position;

	}
}
