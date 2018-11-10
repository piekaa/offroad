using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pieka.Car
{
    class Engine : OrderedScript, IEngine
    {
        public float RPM { get; private set; }
        public float Torque { get; private set; }
        public float Drag { set; private get; }

        public float Throttle { get; set; }

        private bool clutchEngaged;

        [SerializeField]
        private int cylinders = 4;

        [SerializeField]
        private float explosionPower = 1f;

        [SerializeField]
        private Drive drive;

        public IDrive Drive { get; set; }

        void Awake()
        {
            Drive = drive;
        }

        void Start()
        {
            Torque = 0.1f;
            Drag = 10;
        }

        public override void OrderedFixedUpdate()
        {
            float currentExpPower = cylinders * Throttle * explosionPower; 
            Drive.Accelerate(currentExpPower);
        }
    }
}