using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

//todo rename
public class PiekaSerializer
{
    public static string ToJson(object obj)
    {
        return PiekaJsonSerializer.ToJson(obj);
    }

    public static T FromJson<T>(string json)
    {
        return PiekaJsonDeserializer.FromJson<T>(json);
    }
}