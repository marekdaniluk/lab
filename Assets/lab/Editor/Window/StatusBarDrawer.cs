using UnityEngine;
using UnityEditor;
using lab;

public class StatusBarDrawer {

    public delegate void StatusBarDrawerHandler(AiTree aiTree);
    public StatusBarDrawerHandler OnSelectedAiTree = delegate { };

    private AiBehaviour _blackboard;
    private GUIContent _statusBarContent;
    private int _currentTree;

    public AiBehaviour Blackboard {
        set {
            _blackboard = value;
            _currentTree = 0;
            OnSelectedAiTree(CurrentTree);
        }
    }

    public AiTree CurrentTree {
        get { return _blackboard == null ? null : _blackboard.Trees[_currentTree]; }
    }

    public StatusBarDrawer() {
        _currentTree = 0;
        _statusBarContent = new GUIContent();
    }

    public void DrawStatusBar() {
        GUILayout.BeginHorizontal(EditorStyles.toolbar, new GUILayoutOption[0]);
        if (_blackboard != null) {
            _statusBarContent = EditorGUIUtility.ObjectContent(_blackboard, _blackboard.GetType());
            GUILayout.Label(_statusBarContent, "GUIEditor.BreadcrumbLeft", new GUILayoutOption[] { GUILayout.ExpandWidth(false) });
            int index = EditorGUILayout.Popup(_currentTree, EditorUtils.TreesToNames(_blackboard.Trees), "GUIEditor.BreadcrumbMid", new GUILayoutOption[] { GUILayout.ExpandWidth(false) });
            if(index != _currentTree) {
                _currentTree = index;
                OnSelectedAiTree(CurrentTree);
            }
        } else {
            GUILayout.Toggle(true, "none", "GUIEditor.BreadcrumbLeft", new GUILayoutOption[] { GUILayout.ExpandWidth(false) });
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("+", "minibuttonleft", new GUILayoutOption[] { GUILayout.ExpandWidth(false) }) && _blackboard != null) {
            _blackboard.AddTree(new AiTree());
            EditorUtility.SetDirty(_blackboard);
        }
        if (GUILayout.Button("-", "minibuttonright", new GUILayoutOption[] { GUILayout.ExpandWidth(false) }) && _blackboard != null && _blackboard.Trees.Count > 1) {
            _blackboard.RemoveTree(_blackboard.Trees[_currentTree]);
            if (_currentTree == _blackboard.Trees.Count) {
                _currentTree = _blackboard.Trees.Count - 1;
            }
            OnSelectedAiTree(CurrentTree);
            EditorUtility.SetDirty(_blackboard);
        }
        GUILayout.EndHorizontal();
    }
}
