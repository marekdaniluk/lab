using UnityEngine;
using UnityEditor;
using AiBehaviour;

[CustomEditor(typeof(FloatParameterNode))]
public class FloatParameterNodeEditor : Editor {

    private int _index = 0;

    public override void OnInspectorGUI() {
        var parameter = (FloatParameterNode)target;
        if (parameter.Blackboard.FloatParameters.Count == 0) {
            EditorGUILayout.HelpBox(string.Format("Blackboard \"{0}\" has no Float parameters. Add at least one Float parameter.", parameter.Blackboard.name), MessageType.Info);
            return;
        }
        EditorGUILayout.LabelField("Condition:");
        EditorGUILayout.BeginHorizontal();
        string[] keys = parameter.Blackboard.FloatParameters.Keys.ToArray();
        _index = EditorGUILayout.Popup(_index, keys);
        parameter.Key = keys[_index];
        parameter.Condition = (FloatParameterNode.FloatCondition)EditorGUILayout.EnumPopup(parameter.Condition);
        parameter.Value = EditorGUILayout.FloatField(parameter.Value);
        EditorGUILayout.EndHorizontal();
        if (GUI.changed) {
            EditorUtility.SetDirty(target);
        }
    }
}
