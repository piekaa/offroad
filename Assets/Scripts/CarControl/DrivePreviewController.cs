using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Redesign
class DrivePreviewController : Resetable
{
    public Meter speedMeter;

    public PiekaSlider SpeedSlider;

    public PiekaSlider BumpScaleSlider;

    private GameObject floor;

    private SpriteRenderer carSpriteRenderer;

    private SpriteRenderer floorSpriteRenderer;

    // private FakePedal pedal;

    private Queue<GameObject> floors = new Queue<GameObject>();

    public Rigidbody2D CarBody;

    private Vector3 floorInitPos;

    public ToggleButton CruiseControlToggleButton;

    public Pedal AccelerationPedal;

    private bool cruiseControl = true;

    void Awake()
    {
        floor = transform.GetChild(0).gameObject;
        floorInitPos = floor.transform.position;
    }

    protected override void Start()
    {
        base.Start();
        floorSpriteRenderer = floor.GetComponent<SpriteRenderer>();
        // pedal = new FakePedal(AccelerationPedal);
        // CarDriveController.AccelerationPedal = pedal;
        floors.Enqueue(floor);
        BumpScaleSlider.RegisterOnSlide((v) => scaleBumps(v));
        carSpriteRenderer = CarBody.GetComponent<SpriteRenderer>();
        AccelerationPedal.Disable();
        CruiseControlToggleButton.InitialState = cruiseControl;
    }

    void Update()
    {
        var carPositions = SpriteUtils.GetWolrdPositions(carSpriteRenderer);
        var floorPositions = SpriteUtils.GetWolrdPositions(floorSpriteRenderer);
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
}