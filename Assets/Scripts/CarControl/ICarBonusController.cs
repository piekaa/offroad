using System.Collections;
using System.Collections.Generic;
using Pieka.Utils;
using UnityEngine;

namespace Pieka.CarControl
{
    public interface ICarBonusController
    {
        void RegisterOnFlip(Run onBackflip);

        void UnregisterOnFlip(Run onBackflip);
    }
}
