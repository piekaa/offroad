using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CarResetController : Resetable, ICarResetController
{
    private HashSet<KeyValuePair<GameObject, FixedJointData>> gameObjectFixedJointSet = new HashSet<KeyValuePair<GameObject, FixedJointData>>();

    [SerializeField]
    protected Car car;
    public ICar Car;

    protected virtual void Awake()
    {
        Car = car;
        SetTarget(car.gameObject);

        var fixedJoints = car.GetComponentsInChildren<FixedJoint2D>();
        foreach (var joint in fixedJoints)
        {
            gameObjectFixedJointSet.Add(new KeyValuePair<GameObject, FixedJointData>(joint.gameObject, new FixedJointData(joint)));
        }
    }

    public override void Reset()
    {
        base.Reset();
        var newSet = new HashSet<KeyValuePair<GameObject, FixedJointData>>();
        foreach (var entry in gameObjectFixedJointSet)
        {
            Destroy(entry.Value.joint);
            var joint = entry.Key.AddComponent<FixedJoint2D>();
            joint.breakForce = entry.Value.BreakForce;
            joint.breakTorque = entry.Value.BreakTorque;
            joint.connectedBody = entry.Value.ConnectedBody;
            joint.anchor = entry.Value.Anchor;
            joint.connectedAnchor = entry.Value.ConnectedAnchor;
            newSet.Add(new KeyValuePair<GameObject, FixedJointData>(entry.Key, new FixedJointData(joint)));
        }
        gameObjectFixedJointSet = newSet;
    }
}