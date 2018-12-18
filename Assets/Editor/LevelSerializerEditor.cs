using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(LevelSerializer))]
public class LevelSerializerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var myTarget = (LevelSerializer)target;

        if (GUILayout.Button("Serialize"))
            {
                myTarget.Serialize();
            }
    }
}
