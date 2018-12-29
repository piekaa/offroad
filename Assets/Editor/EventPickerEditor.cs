using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(EventPicker))]
public class EventPickerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var myTarget = (EventPicker)target;

        myTarget.EventNames = (EventNames)EditorGUILayout.ObjectField(myTarget.EventNames, typeof(EventNames), false);

        if (myTarget.EventNames == null)
        {
            return;
        }

        myTarget.selectedIndex = EditorGUILayout.Popup(myTarget.selectedIndex, myTarget.EventNames.Events);
        myTarget.Event = myTarget.EventNames.Events[myTarget.selectedIndex];
        EditorUtility.SetDirty(myTarget);
    }
}
