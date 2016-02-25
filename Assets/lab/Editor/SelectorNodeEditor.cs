using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using lab;

[CustomEditor(typeof(SelectorNode))]
public class SelectorNodeEditor : Editor {

    private ReorderableList _list;

    private void OnEnable() {
        _list = new ReorderableList(serializedObject, serializedObject.FindProperty("_nodes"), true, true, false, false);
        _list.drawHeaderCallback += DrawHeader;
        _list.drawElementCallback += DrawElement;
        _list.onReorderCallback += Reorder;
    }

    private void DrawHeader(Rect rect) {
        GUI.Label(rect, "Connections:");
    }

    private void DrawElement(Rect rect, int index, bool isActive, bool isFocused) {
        var element = (ANode)_list.serializedProperty.GetArrayElementAtIndex(index).objectReferenceValue;
        GUI.Label(new Rect(rect.x, rect.y, rect.width - 35, rect.height), string.Format("{0}. {1}", index, element.GetType().Name));
        if (GUI.Button(new Rect(rect.x + rect.width - 35, rect.y, 35, rect.height), "-")) {
            ((SelectorNode)target).RemoveNode(element);
            if (LabWindow.gWindow != null) {
                LabWindow.gWindow.Repaint();
            }
        }
    }

    private void Reorder(ReorderableList list) {
        if (LabWindow.gWindow != null) {
            LabWindow.gWindow.Repaint();
        }
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        _list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
