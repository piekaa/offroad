using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pieka.Car;

namespace Pieka.CarControl
{
    class DrivePreviewController : Resetable
    {
        [SerializeField]
        private CarDriveController carDriveController;
        public ICarDriveController CarDriveController;

        [SerializeField]
        private Meter speedMeter;

        [SerializeField]
        private PiekaSlider speedSlider;
        public IPiekaSlider SpeedSlider;

        [SerializeField]
        private PiekaSlider bumpScaleSlider;
        public IPiekaSlider BumpScaleSlider;

        private GameObject floor;

        private SpriteRenderer carSpriteRenderer;

        private SpriteRenderer floorSpriteRenderer;

        private FakePedal pedal;

        private Queue<GameObject> floors = new Queue<GameObject>();

        [SerializeField]
        private Rigidbody2D carBody;

        private Vector3 carBodyInitialPos;

        private Vector3 floorInitPos;

        void Awake()
        {
            floor = transform.GetChild(0).gameObject;
            floorInitPos = floor.transform.position;
            CarDriveController = carDriveController;
            SpeedSlider = speedSlider;
            BumpScaleSlider = bumpScaleSlider;
        }

        protected override void Start()
        {
            base.Start();
            floorSpriteRenderer = floor.GetComponent<SpriteRenderer>();
            pedal = new FakePedal();
            CarDriveController.AccelerationPedal = pedal;
            floors.Enqueue(floor);
            BumpScaleSlider.RegisterOnSlide((v) => scaleBumps(v));
            carBodyInitialPos = carBody.transform.position;
            carSpriteRenderer = carBody.GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (speedMeter.Value < SpeedSlider.Value)
            {
                pedal.setValue(.6f);
            }
            else
            {
                pedal.setValue(0);
            }
            var carPositions = SpriteUtils.getWolrdPositions(carSpriteRenderer);
            var floorPositions = SpriteUtils.getWolrdPositions(floorSpriteRenderer);
            if (carPositions.TopRight.x >= floorPositions.Center.x)
            {
                floor = Instantiate(floor, floor.transform.position + new Vector3(floorPositions.TopRight.x - floorPositions.TopLeft.x, 0), Quaternion.identity);
                floorSpriteRenderer = floor.GetComponent<SpriteRenderer>();
                floors.Enqueue(floor);
                if (floors.Count > 2)
                {
                    Destroy(floors.Dequeue());
                }
            }

            pedal.Update();

        }

        void scaleBumps(float value)
        {
            var bumps = GameObject.FindObjectsOfType<Bump>();
            foreach (var bump in bumps)
            {
                bump.Scale(value);
            }
        }

        public override void Reset()
        {
            if (floors.Count > 1)
            {
                Destroy(floors.Dequeue());
            }
            floor.transform.position = floorInitPos;
        }

        private class FakePedal : IPedal
        {
            private OnIsPressed onIsPressed;

            public float Value { get; private set; }

            public void RegisterOnIsPressed(OnIsPressed onIsPressed)
            {
                this.onIsPressed += onIsPressed;
            }

            public void Update()
            {
                if (onIsPressed != null)
                {
                    onIsPressed(Value);
                }
            }

            public void setValue(float v)
            {
                Value = v;
            }
        }
    }
}