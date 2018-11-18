using System.Collections;
using System.Collections.Generic;
using Pieka.Laggage;
using Pieka.Utils;
using UnityEngine;

namespace Pieka.Laggage
{
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
}