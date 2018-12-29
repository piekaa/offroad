using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bump : MonoBehaviour
{
    public void Scale(float value)
    {
        transform.localScale = new Vector3(value, value);
    }
}
