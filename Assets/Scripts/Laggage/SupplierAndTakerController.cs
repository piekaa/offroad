using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pieka.Laggage
{
    class SupplierAndTakerController : MonoBehaviour
    {
        [SerializeField]
        private LaggageController laggageController;
        public ILaggageController LaggageController;

        void Start()
        {
            LaggageController = laggageController;
            var triggerControllers = GetComponentsInChildren<TriggerController>();
            foreach (var tc in triggerControllers)
            {
                tc.LaggageController = LaggageController;
            }
        }
    }
}