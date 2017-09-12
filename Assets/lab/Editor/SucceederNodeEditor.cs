using UnityEngine;
using UnityEditor;
using System;

namespace lab.EditorView {
    [CustomEditor(typeof(SucceederNode))]
    public class SucceederNodeEditor : Editor {

        public static Action OnSucceederNodeChanged = delegate { };

        public override void OnInspectorGUI() {
            var parameter = (SucceederNode)target;
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Connection:");
            var node = parameter.GetNode(0);
            if (node != null) {
                GUILayout.Label(string.Format("0. {0}", node.GetType().Name));
                if (GUILayout.Button("-", GUILayout.Width(35))) {
                    parameter.RemoveNode(node);
                    OnSucceederNodeChanged();
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
