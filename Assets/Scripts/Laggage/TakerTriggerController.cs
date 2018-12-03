using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

class TakerTriggerController : Resetable
{

    [SerializeField]
    private Effector2D effector;

    public LaggageController LaggageController;

    public int DelayMillis = 2000;

    private Stopwatch stopwatch = new Stopwatch();

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == Consts.FrontWheelTag)
        {
            stopwatch.Reset();
            stopwatch.Start();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == Consts.FrontWheelTag)
        {
            stopwatch.Stop();
        }
    }

    void FixedUpdate()
    {
        if (stopwatch.ElapsedMilliseconds >= DelayMillis)
        {
            LaggageController.Fly();
            effector.gameObject.SetActive(true);
            stopwatch.Stop();
            stopwatch.Reset();
        }
    }

    public override void Reset()
    {
        effector.gameObject.SetActive(false);
    }
}