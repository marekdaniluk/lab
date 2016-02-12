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
	private IntParamList _intParamList;
	private FloatParamList _floatParamList;
	private BoolParamList _boolParamList;
	private StringParamList _stringParamList;

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
		if(_intParamList != null) {
			_intParamList.DrawParamList();
		}
		if(_floatParamList != null) {
			_floatParamList.DrawParamList();
		}
		if(_boolParamList != null) {
			_boolParamList.DrawParamList();
		}
		if(_stringParamList != null) {
			_stringParamList.DrawParamList();
		}
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
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
			_selected = null;
			_intParamList = null;
			_floatParamList = null;
			_boolParamList = null;
			_stringParamList = null;
        } else if (Selection.activeObject is GameObject) {
            var go = (GameObject)Selection.activeObject;
            if (go.GetComponent<AiController>() != null && go.GetComponent<AiController>().Blackboard != null && go.GetComponent<AiController>().Blackboard != _selected) {
                _selected = go.GetComponent<AiController>().Blackboard;
				_currentTree = 0;
				_intParamList = new IntParamList(_selected);
				_floatParamList = new FloatParamList(_selected);
				_boolParamList = new BoolParamList(_selected);
				_stringParamList = new StringParamList(_selected);
			}
        } else if (Selection.activeObject is AiBlackboard && (AiBlackboard)Selection.activeObject != _selected) {
            _selected = (AiBlackboard)Selection.activeObject;
			_currentTree = 0;
			_intParamList = new IntParamList(_selected);
			_floatParamList = new FloatParamList(_selected);
			_boolParamList = new BoolParamList(_selected);
			_stringParamList = new StringParamList(_selected);
		}
		Repaint();
    }
}
