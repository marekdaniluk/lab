using UnityEngine;
using UnityEditor;
using lab;

[CustomEditor(typeof(StringParameterNode))]
public class StringParameterNodeEditor : Editor {

    private int _index = 0;
    private int _index1 = 0;

    public override void OnInspectorGUI() {
		var parameter = (StringParameterNode)target;
        if (LabWindow._target.StringParameters.Count == 0) {
            EditorGUILayout.HelpBox(string.Format("Behaviour \"{0}\" has no String parameters. Add at least one String parameter.", LabWindow._target.name), MessageType.Info);
            return;
        }
        EditorGUILayout.BeginVertical();
        string[] boolKeys = { "Use Static Value", "Use Dynamic Value" };
        int boolIndex = parameter.DynamicValue ? 1 : 0;
        boolIndex = EditorGUILayout.Popup(boolIndex, boolKeys);
        parameter.DynamicValue = boolIndex == 1 ? true : false;
        EditorGUILayout.LabelField("Condition:");
        EditorGUILayout.BeginHorizontal();
        string[] keys = LabWindow._target.StringParameters.Keys.ToArray<string>();
        for (int i = 0; i < keys.Length; ++i) {
            if (keys[i].Equals(parameter.Key)) {
                _index = i;
            }
            if (keys[i].Equals(parameter.DynamicValueKey)) {
                _index1 = i;
            }
        }
        _index = EditorGUILayout.Popup(_index, keys);
        parameter.Key = keys[_index];
        parameter.Condition = (StringParameterNode.StringCondition)EditorGUILayout.EnumPopup(parameter.Condition);
        if (parameter.DynamicValue) {
            _index1 = EditorGUILayout.Popup(_index1, keys);
            parameter.DynamicValueKey = keys[_index1];
        } else {
            parameter.Value = EditorGUILayout.TextField(parameter.Value);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
        if (GUI.changed) {
            EditorUtility.SetDirty(target);
        }
    }
}
