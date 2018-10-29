using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laggage : Resetable
{

    private List<KeyValuePair<GameObject, Vector3>> laggage = new List<KeyValuePair<GameObject, Vector3>>();

    // Use this for initialization
    void Start()
    {
        var children = GetComponentsInChildren<Rigidbody2D>();
        foreach (var child in children)
        {
            laggage.Add(new KeyValuePair<GameObject, Vector3>(child.gameObject, child.transform.position));
        }
		Debug.Log(laggage.Count);
    }

    public override void Reset()
    {
		Debug.Log("Laggage reset");
        foreach (var item in laggage)
        {
			item.Key.transform.position = item.Value;
        }
    }

}
