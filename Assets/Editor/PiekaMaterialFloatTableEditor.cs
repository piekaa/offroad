using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(PiekaMaterialFloatTable))]
public class PiekaMaterialFloatTableEditor : Editor
{

    private PiekaMaterial material1;
    private PiekaMaterial material2;
    private float floatValue;

    public override void OnInspectorGUI()
    {
        var myTarget = (PiekaMaterialFloatTable)target;

        myTarget.Default = EditorGUILayout.FloatField("Default Float", myTarget.Default);

        EditorGUILayout.Space();

        var dictionary = myTarget.dictionary;

        foreach (var pair in dictionary)
        {
            EditorGUILayout.ObjectField("First", pair.Key.Key, typeof(PiekaMaterial), false);
            EditorGUILayout.ObjectField("Second", pair.Key.Value, typeof(PiekaMaterial), false);
            EditorGUILayout.FloatField("Float", pair.Value);
            if (GUILayout.Button("Delete"))
            {
                dictionary.Remove(pair.Key);
            }
            EditorGUILayout.Space();
        }

        EditorGUILayout.Space();

        material1 = (PiekaMaterial)EditorGUILayout.ObjectField("First", material1, typeof(PiekaMaterial), false);
        material2 = (PiekaMaterial)EditorGUILayout.ObjectField("Second", material2, typeof(PiekaMaterial), false);
        floatValue = EditorGUILayout.FloatField("Float", floatValue);

        if (GUILayout.Button("Add"))
        {
            dictionary.Add(new MaterialMaterialPair(material1, material2), floatValue);
        }
    }
}