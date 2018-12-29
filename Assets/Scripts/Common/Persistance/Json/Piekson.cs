using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

public class Piekson
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