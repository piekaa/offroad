using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderedScriptExecutor : MonoBehaviour {

	[SerializeField]
	private List<OrderedScript> orderedScripts;

	void FixedUpdate() {
		foreach( var script in orderedScripts ) 
		{
			script.OrderedFixedUpdate ();
		}
	}

}
