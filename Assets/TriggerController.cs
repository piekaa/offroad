using System.Collections;
using System.Collections.Generic;
using Pieka.Laggage;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    [SerializeField]
    private LaggageController laggageController;
    public ILaggageController LaggageController;

    public void Start()
    {
        LaggageController = laggageController;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == Consts.CarLayer)
        {
            LaggageController.Release();
        }
    }
}
