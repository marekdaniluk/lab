using UnityEngine;
using UnityEditor;

namespace lab.EditorView {
    [CustomEditor(typeof(TaskNode))]
    public class TaskNodeEditor : Editor {

        private int _index = 0;

        public override void OnInspectorGUI() {
            var parameter = (TaskNode)target;
            if (LabWindow._target.Blackboard.TaskParameters.Count == 0) {
                EditorGUILayout.HelpBox(string.Format("Behaviour \"{0}\" has no Task parameters. Add at least one Task parameter.", LabWindow._target.name), MessageType.Info);
                return;
            }
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            string[] keys = LabWindow._target.Blackboard.TaskParameters.Keys;
            for (int i = 0; i < keys.Length; ++i) {
                if (keys[i].Equals(parameter.TaskKey)) {
                    _index = i;
                }
            }
            _index = EditorGUILayout.Popup(_index, keys);
            parameter.TaskKey = keys[_index];
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            if (GUI.changed) {
                EditorUtility.SetDirty(target);
            }
        }
    }
}
