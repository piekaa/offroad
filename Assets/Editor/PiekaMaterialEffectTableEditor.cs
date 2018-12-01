using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(PiekaMaterialEffectTable))]
public class PiekaMaterialEffectTableEditor : Editor
{

    private PiekaMaterial material1;
    private PiekaMaterial material2;
    private Effect effect;

    public override void OnInspectorGUI()
    {
        var myTarget = (PiekaMaterialEffectTable)target;

        // EditorGUILayout.BeginHorizontal();

        myTarget.defaultEffect = (Effect)EditorGUILayout.ObjectField("Default effect", myTarget.defaultEffect, typeof(Effect), false);

        EditorGUILayout.Space();

        var dictionary = myTarget.dictionary;

        foreach (var pair in dictionary)
        {
            EditorGUILayout.ObjectField("First", pair.Key.Key, typeof(PiekaMaterial), false);
            EditorGUILayout.ObjectField("Second", pair.Key.Value, typeof(PiekaMaterial), false);
            EditorGUILayout.ObjectField("Effect", pair.Value, typeof(Effect), false);
            if (GUILayout.Button("Delete"))
            {
                dictionary.Remove(pair.Key);
            }
            EditorGUILayout.Space();
        }

        EditorGUILayout.Space();

        material1 = (PiekaMaterial)EditorGUILayout.ObjectField("First", material1, typeof(PiekaMaterial), false);
        material2 = (PiekaMaterial)EditorGUILayout.ObjectField("Second", material2, typeof(PiekaMaterial), false);
        effect = (Effect)EditorGUILayout.ObjectField("Effect", effect, typeof(Effect), false);

        if (GUILayout.Button("Add"))
        {
            dictionary.Add(new MaterialMaterialPair(material1, material2), effect);
        }

        // EditorGUILayout.EndHorizontal();

        // myTarget.experience = EditorGUILayout.IntField("Experience", myTarget.experience);
        // EditorGUILayout.LabelField("Level", myTarget.Level.ToString());
    }
}