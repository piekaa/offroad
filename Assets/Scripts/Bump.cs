using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bump : MonoBehaviour
{

	private float ratio;

	void Start()
	{
		ratio = transform.localScale.y / transform.localScale.x;
	}

    public void Scale(float value)
    {
		transform.localScale = new Vector3(value / ratio, value);
    }
}
