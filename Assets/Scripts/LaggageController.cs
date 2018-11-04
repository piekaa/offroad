using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaggageController : Resetable {
	[SerializeField]
    private GameObject laggage;

    void Awake()
    {
		SetTarget(laggage);
    }
}
