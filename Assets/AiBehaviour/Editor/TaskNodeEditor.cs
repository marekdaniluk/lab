using UnityEngine;
using UnityEditor;
using AiBehaviour;

[CustomEditor(typeof(TaskNode))]
public class TaskNodeEditor : Editor {

    public override void OnInspectorGUI() {
        var parameter = (TaskNode)target;
        parameter.TaskIndex = EditorGUILayout.IntField("Task index:", parameter.TaskIndex);
        if (GUI.changed) {
            EditorUtility.SetDirty(target);
        }
    }
}
