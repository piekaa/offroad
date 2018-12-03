using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SupplierTriggerController : TriggerController
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == Consts.CarLayer)
        {
            LaggageController.Release();
        }
    }
}
