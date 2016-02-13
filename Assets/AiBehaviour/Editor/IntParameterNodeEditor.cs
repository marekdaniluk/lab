using UnityEngine;
using UnityEditor;
using AiBehaviour;

[CustomEditor(typeof(IntParameterNode))]
public class IntParameterNodeEditor : Editor {

    private int _index = 0;

    public override void OnInspectorGUI() {
        var parameter = (IntParameterNode)target;
        if (parameter.Blackboard.IntParameters.Count == 0) {
            EditorGUILayout.HelpBox(string.Format("Blackboard \"{0}\" has no Int parameters. Add at least one Int parameter.", parameter.Blackboard.name), MessageType.Info);
            return;
        }
        EditorGUILayout.LabelField("Condition:");
        EditorGUILayout.BeginHorizontal();
        string[] keys = parameter.Blackboard.IntParameters.Keys.ToArray();
        _index = EditorGUILayout.Popup(_index, keys);
        parameter.Key = keys[_index];
        parameter.Condition = (IntParameterNode.IntCondition)EditorGUILayout.EnumPopup(parameter.Condition);
        parameter.Value = EditorGUILayout.IntField(parameter.Value);
        EditorGUILayout.EndHorizontal();
        if (GUI.changed) {
            EditorUtility.SetDirty(target);
        }
    }
}
