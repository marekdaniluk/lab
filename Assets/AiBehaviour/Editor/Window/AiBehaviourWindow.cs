using UnityEngine;
using UnityEditor;
using AiBehaviour;

public class AiBehaviourWindow : EditorWindow {

    private AiBlackboard _selected;
    private Vector2 _scrollPosition = Vector2.zero;
    private bool _resize = false;
    private float _minimumViewWidth = 150f;
    private float _currentViewWidth;
    private Rect _cursorChangeRect;
    private StatusBarDrawer _statusBar;
    private ParamPanelDrawer _paramPanel;

    [MenuItem("Window/AiBehaviour")]
    public static void ShowEditor() {
        EditorWindow.GetWindow<AiBehaviourWindow>();
    }

    public void OnEnable() {
        titleContent = new GUIContent("AiBehaviour");
        titleContent.image = (Texture2D)EditorGUIUtility.Load("Assets/AiBehaviour/Icons/AiController.png");
        _statusBar = new StatusBarDrawer();
        _paramPanel = new ParamPanelDrawer();
        _currentViewWidth = 200f;
        _cursorChangeRect = new Rect(_currentViewWidth, 0f, 5f, position.height);
    }

    private void OnGUI() {
        _statusBar.DrawStatusBar();
        ResizeSplitPanel();
        _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, GUILayout.Width(_currentViewWidth), GUILayout.Height(position.height));
        _paramPanel.DrawPanel();
        GUILayout.EndScrollView();
        GUI.BeginGroup(new Rect(_currentViewWidth, EditorStyles.toolbar.fixedHeight, position.width - _currentViewWidth, position.height), string.Empty, "AnimationCurveEditorBackground");
        EditorUtils.DrawGrid(position);
        GUI.EndGroup();
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
        var go = Selection.activeObject as GameObject;
        if (go != null && go.GetComponent<AiController>() != null && go.GetComponent<AiController>().Blackboard != null) {
            if (go.GetComponent<AiController>().Blackboard != _selected) {
                _selected = go.GetComponent<AiController>().Blackboard;
                _statusBar.Blackboard = _selected;
                _paramPanel.Blackboard = _selected;
            }
        } else if (Selection.activeObject is AiBlackboard) {
            if ((AiBlackboard)Selection.activeObject != _selected) {
                _selected = (AiBlackboard)Selection.activeObject;
                _statusBar.Blackboard = _selected;
                _paramPanel.Blackboard = _selected;
            }
        } else {
            _selected = null;
            _statusBar.Blackboard = null;
            _paramPanel.Blackboard = null;
        }
        Repaint();
    }
}
