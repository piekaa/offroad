using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pieka.Car
{
    class Wheel : MonoBehaviour, IWheel
    {

        private Rigidbody2D rb;

        //todo maybe take that from sprite
        public float DiameterInMeters { get; set; }

        [SerializeField]
        private float diameterInMeters;

        public void Awake()
        {
            DiameterInMeters = diameterInMeters;
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