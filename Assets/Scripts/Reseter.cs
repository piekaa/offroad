using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reseter : MonoBehaviour
{
    Resetable[] resetables;
    void Start()
    {
        resetables = GameObject.FindObjectsOfType<Resetable>();
    }

    public void Reset() {
        foreach(var resetable in resetables) 
        {
            resetable.Reset();
        }
    }

}
