using UnityEngine;
using UnityEditor;
using lab;

[CustomEditor(typeof(BoolParameterNode))]
public class BoolParameterNodeEditor : Editor {

    private int _index = 0;
    private int _index1 = 0;

    public override void OnInspectorGUI() {
		var parameter = (BoolParameterNode)target;
        if (LabWindow._target.BoolParameters.Count == 0) {
            EditorGUILayout.HelpBox(string.Format("Behaviour \"{0}\" has no Bool parameters. Add at least one Bool parameter.", LabWindow._target.name), MessageType.Info);
            return;
        }
        EditorGUILayout.BeginVertical();
        string[] boolKeys = { "Use Static Value", "Use Dynamic Value" };
        int boolIndex = parameter.DynamicValue ? 1 : 0;
        boolIndex = EditorGUILayout.Popup(boolIndex, boolKeys);
        parameter.DynamicValue = boolIndex == 1 ? true : false;
        EditorGUILayout.LabelField("Condition:");
        EditorGUILayout.BeginHorizontal();
        string[] keys = LabWindow._target.BoolParameters.Keys.ToArray<bool>();
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
        if (parameter.DynamicValue) {
            parameter.Condition = (BoolParameterNode.BoolCondition)EditorGUILayout.EnumPopup(parameter.Condition);
            _index1 = EditorGUILayout.Popup(_index1, keys);
            parameter.DynamicValueKey = keys[_index1];
        } else {
            string[] boolKeysValue = { "false", "true" };
            int boolIndexValue = parameter.Value ? 1 : 0;
            boolIndexValue = EditorGUILayout.Popup(boolIndexValue, boolKeysValue);
            parameter.Value = boolIndexValue == 1 ? true : false;
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
        if (GUI.changed) {
            EditorUtility.SetDirty(target);
        }
    }
}
