using UnityEngine;
using UnityEditor;
using lab;

[CustomEditor(typeof(TreeNode))]
public class TreeNodeEditor : Editor {

    public override void OnInspectorGUI() {
        var parameter = (TreeNode)target;
        string[] keys = LabWindow._target.Trees.ToArray();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Tree index:");
        parameter.TreeIndex = EditorGUILayout.Popup(parameter.TreeIndex, keys);
        EditorGUILayout.EndHorizontal();
        if (GUI.changed) {
            EditorUtility.SetDirty(target);
        }
    }
}
