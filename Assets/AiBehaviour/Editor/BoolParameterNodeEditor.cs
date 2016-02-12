using UnityEngine;
using UnityEditor;
using AiBehaviour;

[CustomEditor(typeof(BoolParameterNode))]
public class BoolParameterNodeEditor : Editor {

    private int _index = 0;

    public override void OnInspectorGUI() {
        var parameter = (BoolParameterNode)target;
        if (parameter.Blackboard.BoolParameters.Count == 0) {
            EditorGUILayout.HelpBox(string.Format("Blackboard \"{0}\" has no Bool parameters. Add at least one Bool parameter.", parameter.Blackboard.name), MessageType.Info);
            return;
        }
        EditorGUILayout.LabelField("Condition:");
        EditorGUILayout.BeginHorizontal();
        string[] keys = parameter.Blackboard.BoolParameters.Keys.ToArray();
        _index = EditorGUILayout.Popup(_index, keys);
        parameter.Key = keys[_index];
        string[] boolKeys = { "false", "true" };
        int boolIndex = parameter.Value ? 1 : 0;
        boolIndex = EditorGUILayout.Popup(boolIndex, boolKeys);
        parameter.Value = boolIndex == 1 ? true : false;
        EditorGUILayout.EndHorizontal();
        if (GUI.changed) {
            EditorUtility.SetDirty(target);
        }
    }
}
