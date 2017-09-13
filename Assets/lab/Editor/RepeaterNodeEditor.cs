using UnityEngine;
using UnityEditor;
using System;

namespace lab.EditorView {
    [CustomEditor(typeof(RepeaterNode))]
    public class RepeaterNodeEditor : Editor {

        public override void OnInspectorGUI() {
            var parameter = (RepeaterNode)target;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Repeat:");
            parameter.Repeat = EditorGUILayout.IntField(parameter.Repeat);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Connection:");
            var node = parameter.GetNode(0);
            if (node != null) {
                GUILayout.Label(string.Format("0. {0}", node.GetType().Name));
                if (GUILayout.Button("-", GUILayout.Width(35))) {
                    parameter.RemoveNode(node);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
