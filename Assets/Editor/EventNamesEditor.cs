using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(EventNames))]
public class EventNamesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var myTarget = (EventNames)target;

        var events = myTarget.Events;

        for (int i = 0; i < events.Length; i++)
        {
            EditorGUILayout.LabelField(events[i]);
        }
    }
}
