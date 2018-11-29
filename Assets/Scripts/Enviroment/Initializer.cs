using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{

    public Initializable[] Initializables;

    void Start()
    {
        foreach (var initializable in Initializables)
        {
            initializable.Init();
        }
    }
}
