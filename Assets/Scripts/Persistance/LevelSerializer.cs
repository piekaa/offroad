using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSerializer : MonoBehaviour
{
    public void Serialize()
    {
        Debug.Log("Serialization");

        var size = transform.childCount;

        for (int i = 0; i < size; i++)
        {
            var child = transform.GetChild(i);
            Debug.Log(JsonUtility.ToJson(child));
        }
    }

}
