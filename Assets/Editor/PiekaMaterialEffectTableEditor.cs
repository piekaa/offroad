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

        myTarget.defaultEffect = (Effect)EditorGUILayout.ObjectField("Default effect", myTarget.defaultEffect, typeof(Effect), false);

        EditorGUILayout.Space();
        var dictionary = myTarget.dictionary;
        var keys = new List<MaterialMaterialPair>(dictionary.Keys);

        foreach (var key in keys)
        {
            EditorGUILayout.ObjectField("First", key.Key, typeof(PiekaMaterial), false);
            EditorGUILayout.ObjectField("Second", key.Value, typeof(PiekaMaterial), false);
            dictionary[key] = (Effect)EditorGUILayout.ObjectField("Effect", dictionary[key], typeof(Effect), false);
            if (GUILayout.Button("Delete"))
            {
                dictionary.Remove(key);
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

        EditorUtility.SetDirty(myTarget);
    }
}
