using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using AiBehaviour;

public class AiBehaviourWindow : EditorWindow {

    private AiBlackboard _selected;
    private int _currentTree = 0;

    private Vector2 _scrollPosition = Vector2.zero;
    private bool _resize = false;
    private float _minimumViewWidth = 150f;
    private float _currentViewWidth;
    private Rect _cursorChangeRect;
    private GUIContent _statusBarContent;
    private ReorderableList _list;
    private SerializedObject _serializedObject;

    [MenuItem("Window/AiBehaviour")]
    public static void ShowEditor() {
        EditorWindow.GetWindow<AiBehaviourWindow>();
    }

    public void OnEnable() {
        titleContent = new GUIContent("AiBehaviour");
        titleContent.image = (Texture2D)EditorGUIUtility.Load("Assets/AiBehaviour/Icons/AiController.png");
        _currentViewWidth = 200f;
        _cursorChangeRect = new Rect(_currentViewWidth, 0f, 5f, position.height);
        _statusBarContent = new GUIContent();
    }

    private void OnGUI() {
        ResizeSplitPanel();
        DrawStatusBar();
        _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, GUILayout.Width(_currentViewWidth), GUILayout.Height(position.height));
        DrawParameterList();
        GUILayout.EndScrollView();
        GUI.BeginGroup(new Rect(_currentViewWidth, EditorStyles.toolbar.fixedHeight, position.width - _currentViewWidth, position.height), string.Empty, "AnimationCurveEditorBackground");
        EditorUtils.DrawGrid(position);
        GUI.EndGroup();
    }

    private void DrawStatusBar() {
        GUILayout.BeginHorizontal(EditorStyles.toolbar, new GUILayoutOption[0]);
        if (_selected != null) {
            _statusBarContent = EditorGUIUtility.ObjectContent(_selected, _selected.GetType());
            GUILayout.Label(_statusBarContent, "GUIEditor.BreadcrumbLeft", new GUILayoutOption[] { GUILayout.ExpandWidth(false) });
            _currentTree = EditorGUILayout.Popup(_currentTree, EditorUtils.TreesToNames(_selected.Trees), "GUIEditor.BreadcrumbMid", new GUILayoutOption[] { GUILayout.ExpandWidth(false) });
        } else {
            GUILayout.Toggle(true, "none", "GUIEditor.BreadcrumbLeft", new GUILayoutOption[] { GUILayout.ExpandWidth(false) });
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("+", "minibuttonleft", new GUILayoutOption[] { GUILayout.ExpandWidth(false) }) && _selected != null) {
            _selected.AddTree(new AiTree());
            EditorUtility.SetDirty(_selected);
        }
        if (GUILayout.Button("-", "minibuttonright", new GUILayoutOption[] { GUILayout.ExpandWidth(false) }) && _selected != null && _selected.Trees.Count > 1) {
            _selected.RemoveTree(_selected.Trees[_currentTree]);
            if (_currentTree == _selected.Trees.Count) {
                _currentTree = _selected.Trees.Count - 1;
            }
            EditorUtility.SetDirty(_selected);
        }
        GUILayout.EndHorizontal();
    }

    private void DrawParameterList() {
        if(_serializedObject != null) {
            _serializedObject.Update();
            _list.DoLayoutList();
            _serializedObject.ApplyModifiedProperties();
        }
    }

    private void ResizeSplitPanel() {
        EditorGUIUtility.AddCursorRect(_cursorChangeRect, MouseCursor.ResizeHorizontal);
        if (Event.current.type == EventType.mouseDown && _cursorChangeRect.Contains(Event.current.mousePosition)) {
            _resize = true;
        }
        if (_resize) {
            _currentViewWidth = Mathf.Clamp(Event.current.mousePosition.x, _minimumViewWidth, position.width - _minimumViewWidth);
            _cursorChangeRect.Set(_currentViewWidth, 0f, 5f, position.height);
            Repaint();
        }
        if (Event.current.type == EventType.MouseUp) {
            _resize = false;
        }
    }

    private void OnFocus() {
        OnSelectionChange();
    }

    private void OnSelectionChange() {
        if (Selection.activeObject == null) {
            return;
        }
        if (Selection.activeObject is GameObject) {
            var go = (GameObject)Selection.activeObject;
            if (go.GetComponent<AiController>() != null && go.GetComponent<AiController>().Blackboard != null && go.GetComponent<AiController>().Blackboard != _selected) {
                _selected = go.GetComponent<AiController>().Blackboard;
                _currentTree = 0;
                Repaint();
            }
        } else if (Selection.activeObject is AiBlackboard && (AiBlackboard)Selection.activeObject != _selected) {
            _selected = (AiBlackboard)Selection.activeObject;
            _currentTree = 0;
            Repaint();
        }
        if (_selected != null) {
            _serializedObject = new SerializedObject(_selected);
            var p = _serializedObject.FindProperty("_intParameters");
            _list = new ReorderableList(_serializedObject, p.FindPropertyRelative("_keys"), true, true, true, true);
            _list.drawHeaderCallback += rect => GUI.Label(rect, p.displayName);
            _list.onAddCallback += reorderableList => {
                string key = "Int Parameter";
                string k = key;
                int i = 0;
                while (_selected.IntParameters.ContainsKey(k)) {
                    k = key + " " + (i++).ToString();
                }
                _selected.IntParameters[k] = 0;
            };
            _list.onRemoveCallback += reorderableList => {
                _selected.IntParameters.Remove(_list.serializedProperty.GetArrayElementAtIndex(_list.index).stringValue);
            };
            _list.drawElementCallback += (rect, index, active, focused) =>
            {
                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width - 35, rect.height), p.FindPropertyRelative("_keys").GetArrayElementAtIndex(index), GUIContent.none);
                EditorGUI.PropertyField(new Rect(rect.x + rect.width - 35, rect.y, 35, rect.height), p.FindPropertyRelative("_values").GetArrayElementAtIndex(index), GUIContent.none);
            };
        }
    }
}
