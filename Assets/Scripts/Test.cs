using UnityEngine;
using Pieka.Persistance;
using Pieka.Data;

public class Test : MonoBehaviour
{
    void Awake()
    {
        Repository.Save(new TestData());
        Debug.Log(Repository.Load<TestData>());
    }
}