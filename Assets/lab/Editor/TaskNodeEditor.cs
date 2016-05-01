using UnityEngine;
using UnityEditor;
using lab;

[CustomEditor(typeof(TaskNode))]
public class TaskNodeEditor : Editor {

    public override void OnInspectorGUI() {
		var parameter = (TaskNode)target;
		parameter.TaskKey = EditorGUILayout.TextField("Task key:", parameter.TaskKey);
        if (GUI.changed) {
            EditorUtility.SetDirty(target);
        }
    }
}
