using System.Collections;
using UnityEngine;
public abstract class CarStateDetector : ScriptableObject
{
    public void StartDetection(Car car)
    {
        car.StartCoroutine(detectCoroutine(car));
    }

    protected abstract IEnumerator detectCoroutine(Car car);
}