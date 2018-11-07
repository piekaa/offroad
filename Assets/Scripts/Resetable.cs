using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetable : MonoBehaviour
{
    private List<KeyValuePair<Rigidbody2D, Vector3>> rigidbodiesWithPositions = new List<KeyValuePair<Rigidbody2D, Vector3>>();
    private GameObject target;
    // Use this for initialization
    protected virtual void Start()
    { 
        target = target == null ? gameObject : target;
        var children = target.GetComponentsInChildren<Rigidbody2D>();
        foreach (var child in children)
        {
            rigidbodiesWithPositions.Add(new KeyValuePair<Rigidbody2D, Vector3>(child, child.transform.position));
        }
    }

    public virtual void Reset()
    {
        foreach (var item in rigidbodiesWithPositions)
        {
            item.Key.transform.position = item.Value;
            item.Key.transform.rotation = Quaternion.identity;
            item.Key.velocity = Vector3.zero;
            item.Key.angularVelocity = 0;
        }
    }

    // Set in Awake method
    protected virtual void SetTarget(GameObject target)
    { 
        this.target = target;
    }
}
