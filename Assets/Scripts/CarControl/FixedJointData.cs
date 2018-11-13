using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pieka.CarControl
{
    class FixedJointData
    {
        public FixedJoint2D joint;
        public float BreakForce;
        public float BreakTorque;
        public Rigidbody2D ConnectedBody;
        public Vector2 Anchor;
        public Vector2 ConnectedAnchor;

        public FixedJointData(FixedJoint2D joint)
        {
            this.joint = joint;
            BreakForce = joint.breakForce;
            BreakTorque = joint.breakTorque;
            ConnectedBody = joint.connectedBody;
            Anchor = joint.anchor;
            ConnectedAnchor = joint.connectedAnchor;
        }
    }
}