using UnityEngine;
using UnityEditor;
using AiBehaviour;

[CustomEditor(typeof(StringParameterNode))]
public class StringParameterNodeEditor : Editor {

    private int _index = 0;

    public override void OnInspectorGUI() {
        var parameter = (StringParameterNode)target;
        if (parameter.Blackboard.StringParameters.Count == 0) {
            EditorGUILayout.HelpBox(string.Format("Blackboard \"{0}\" has no String parameters. Add at least one String parameter.", parameter.Blackboard.name), MessageType.Info);
            return;
        }
        EditorGUILayout.LabelField("Condition:");
        EditorGUILayout.BeginHorizontal();
        string[] keys = parameter.Blackboard.StringParameters.Keys.ToArray();
        _index = EditorGUILayout.Popup(_index, keys);
        parameter.Key = keys[_index];
        parameter.Condition = (StringParameterNode.StringCondition)EditorGUILayout.EnumPopup(parameter.Condition);
        parameter.Value = EditorGUILayout.TextField(parameter.Value);
        EditorGUILayout.EndHorizontal();
        if (GUI.changed) {
            EditorUtility.SetDirty(target);
        }
    }
}
