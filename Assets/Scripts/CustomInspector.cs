using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[UnityEditor.CustomEditor(typeof(TragectoryCalculator))]
public class CustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TragectoryCalculator obj = (TragectoryCalculator)target;

        if(GUILayout.Toggle(false, "auto Update"))
        {
            obj.autoUpdate = true;
        }
        if(GUILayout.Button("Recalculate Trtagectory"))
        {
            obj.CalculateTragectory();
        }
    }
}
