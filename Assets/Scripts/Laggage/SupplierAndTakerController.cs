using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplierAndTakerController : MonoBehaviour
{
    public LaggageController LaggageController;

    void Start()
    {
        var triggerControllers = GetComponentsInChildren<TriggerController>();
        foreach (var tc in triggerControllers)
        {
            tc.LaggageController = LaggageController;
        }
    }
}
