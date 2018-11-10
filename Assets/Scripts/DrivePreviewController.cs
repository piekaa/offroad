using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pieka.Car;

public class DrivePreviewController : Resetable
{

    [SerializeField]
    private GameObject laggage;

    [SerializeField]
    private CarDriveController carDriveController;
    public ICarDriveController CarDriveController;

    [SerializeField]
    private Car car;

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

    private Rigidbody2D carBody;

    private List<KeyValuePair<Rigidbody2D, Vector3>> laggageRigidbodiesWithPositions = new List<KeyValuePair<Rigidbody2D, Vector3>>();

    private Vector3 carBodyInitialPos;

    void Awake()
    {
        CarDriveController = carDriveController;
        SpeedSlider = speedSlider;
        BumpScaleSlider = bumpScaleSlider;
        carBody = car.GetComponentInChildren<Rigidbody2D>();

        var children = laggage.GetComponentsInChildren<Rigidbody2D>();
        foreach (var child in children)
        {
            laggageRigidbodiesWithPositions.Add(new KeyValuePair<Rigidbody2D, Vector3>(child, child.transform.position));
        }

    }

    protected override void Start()
    {
        base.Start();
        floor = transform.GetChild(0).gameObject;
        carSpriteRenderer = car.GetComponentInChildren<SpriteRenderer>();
        floorSpriteRenderer = floor.GetComponent<SpriteRenderer>();
        pedal = new FakePedal();
        CarDriveController.AccelerationPedal = pedal;
        floors.Enqueue(floor);
        BumpScaleSlider.RegisterOnSlide((v) => scaleBumps(v));
        carBodyInitialPos = carBody.transform.position;
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
            if (floors.Count > 3)
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
    public override void Reset()
    {
        var translationX = carBody.transform.position.x - carBodyInitialPos.x;
        carBody.transform.position = new Vector3(carBody.transform.position.x, carBodyInitialPos.y);
        carBody.transform.rotation = Quaternion.identity;
        resetRigiedbodies(car.GetComponentsInChildren<Rigidbody2D>());
        foreach (var item in laggageRigidbodiesWithPositions)
        {
            item.Key.transform.position = item.Value + new Vector3(translationX, 0);
            item.Key.transform.rotation = Quaternion.identity;
            item.Key.velocity = Vector3.zero;
            item.Key.angularVelocity = 0;
        }
    }

    private void resetRigiedbodies(Rigidbody2D[] rigidbodies)
    {
        foreach (var rb in rigidbodies)
        {
            rb.angularVelocity = 0;
            rb.velocity = Vector3.zero;
        }
    }
}