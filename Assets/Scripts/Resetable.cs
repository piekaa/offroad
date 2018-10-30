using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetable : MonoBehaviour
{
    private List<KeyValuePair<Rigidbody2D, Vector3>> laggage = new List<KeyValuePair<Rigidbody2D, Vector3>>();

    // Use this for initialization
    void Start()
    {
        var children = GetComponentsInChildren<Rigidbody2D>();
        foreach (var child in children)
        {
            laggage.Add(new KeyValuePair<Rigidbody2D, Vector3>(child, child.transform.position));
        }
        Debug.Log(laggage.Count);
    }

    public virtual void Reset()
    {
        foreach (var item in laggage)
        {
            item.Key.transform.position = item.Value;
            item.Key.transform.rotation = Quaternion.identity;
            item.Key.velocity = Vector3.zero;
            item.Key.angularVelocity = 0;
        }
    }
}
