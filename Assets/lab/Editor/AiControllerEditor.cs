using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;
using lab;

[CustomEditor(typeof(AiController))]
public class AiControllerEditor : Editor {

    private ReorderableList _list;

    private void OnEnable() {
        _list = new ReorderableList(serializedObject, serializedObject.FindProperty("_tasks"), false, true, true, true);
        _list.drawHeaderCallback += DrawHeader;
        _list.drawElementCallback += DrawElement;
    }

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        var aiController = (AiController)target;
        if (aiController.Behaviour == null) {
            EditorGUILayout.HelpBox("Not initialized", MessageType.Info);
            return;
        }
        serializedObject.Update();
        _list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawHeader(Rect rect) {
        GUI.Label(rect, "Task binding:");
    }

    private void DrawElement(Rect rect, int index, bool isActive, bool isFocused) {
        var element = _list.serializedProperty.GetArrayElementAtIndex(index);
        var w = rect.width / 3f;
        var trees = ((AiController)target).Behaviour.Trees;
        var keys = new List<string>();
        for(int i = 0; i < trees.Count; ++i) {
            keys.AddRange(trees[i].GetTaskNodeKeys());
        }
        var idx = 0;
        for(int i = 0; i < keys.Count; ++i) {
            if(element.FindPropertyRelative("taskKeyName").stringValue == keys[i]) {
                idx = i;
            }
        }
        idx = EditorGUI.Popup(new Rect(rect.x + 5, rect.y, w - 5, EditorGUIUtility.singleLineHeight), idx, keys.ToArray());
        element.FindPropertyRelative("taskKeyName").stringValue = keys[idx];
        EditorGUI.PropertyField(new Rect(rect.x + 5 + w, rect.y, rect.width - w - 5, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("task"), GUIContent.none);
    }
}
