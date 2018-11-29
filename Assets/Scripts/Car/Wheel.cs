using System.Collections;
using System.Collections.Generic;
using Pieka.Utils;
using UnityEngine;

namespace Pieka.Car
{
    public class Wheel : MonoBehaviour, IWheel
    {

        private Rigidbody2D rb;

        public float DiameterInMeters { get; private set; }

        public void Awake()
        {
            DiameterInMeters = SpriteUtils.GetWolrdPositions(GetComponent<SpriteRenderer>()).Width * Consts.MetersPerWroldUnit;
            rb = GetComponent<Rigidbody2D>();
        }

        public void AddTorque(float torque)
        {
            rb.AddTorque(torque);
        }

        public float AngularDrag
        {
            get
            {
                return rb.angularDrag;
            }

            set
            {
                rb.angularDrag = value;
            }
        }

        public float AngularVelocity
        {
            get
            {
                return rb.angularVelocity;
            }

            set
            {
                rb.angularVelocity = value;
            }
        }
    }
}