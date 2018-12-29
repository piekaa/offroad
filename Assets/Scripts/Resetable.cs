using System.Collections.Generic;
using UnityEngine;
using UnityScript.Scripting.Pipeline;

public class Resetable : PiekaBehaviour
{
    private List<KeyValuePair<Rigidbody2D, Vector3>> rigidbodiesWithPositions;
    private GameObject target;

    protected virtual void Start()
    {
        Init();
    }

    protected void Init()
    {
        rigidbodiesWithPositions = new List<KeyValuePair<Rigidbody2D, Vector3>>();
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
            var t = item.Key.transform;
            t.position = item.Value;
            t.rotation = Quaternion.identity;
            item.Key.velocity = Vector3.zero;
            item.Key.angularVelocity = 0;
        }
    }
 
    protected void SetTarget(GameObject t)
    {
        target = t;
    }
}