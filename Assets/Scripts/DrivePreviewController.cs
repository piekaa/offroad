using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivePreviewController : MonoBehaviour
{
    [SerializeField]
    private Car car;
    [SerializeField]
    private Meter speedMeter;
    private GameObject floor;
    private GameObject currentFloor;
    private SpriteRenderer carSpriteRenderer;
    private SpriteRenderer floorSpriteRenderer;
    private FakePedal pedal;
    Queue<GameObject> floors = new Queue<GameObject>();
    void Start()
    {
        Debug.Log("Start");
        floor = transform.GetChild(0).gameObject;
        currentFloor = floor;
        carSpriteRenderer = car.GetComponentInChildren<SpriteRenderer>();
        floorSpriteRenderer = floor.GetComponent<SpriteRenderer>();
        pedal = new FakePedal();
        car.GetComponentInChildren<Engine>().AccelerationPedal = pedal;
        floors.Enqueue(floor);
    }
    void Update()
    {
        if (speedMeter.Value < 20)
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
    }
    private class FakePedal : IPedal
    {
        public float Value { get; private set; }
        public void setValue(float v)
        {
            Value = v;
        }
    }
}