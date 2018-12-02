using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICarBonusController
{
    void RegisterOnFlip(Run onBackflip);

    void UnregisterOnFlip(Run onBackflip);
}
