using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetable : MonoBehaviour
{
    private Vector3 position;

    void Start()
    {
        position = transform.position;
        Debug.Log(position);
    }

    public virtual void Reset()
    {
        transform.position = position; 
    }
}
