using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pieka.Laggage
{
    public class LaggageController : Resetable, ILaggageController
    {
        [SerializeField]
        private GameObject laggage;

        void Awake()
        {
            SetTarget(laggage);
            var rigidbodies = laggage.GetComponentsInChildren<Rigidbody2D>();
            foreach (var rb in rigidbodies)
            {
                rb.gravityScale = 0;
            }
        }

        public override void Reset()
        {
            base.Reset();
            var rigidbodies = laggage.GetComponentsInChildren<Rigidbody2D>();
            foreach (var rb in rigidbodies)
            {
                rb.gravityScale = 0;
            }
        }

        public void Release()
        {
            var rigidbodies = laggage.GetComponentsInChildren<Rigidbody2D>();
            foreach (var rb in rigidbodies)
            {
                rb.gravityScale = 1;
            }
        }
    }
}