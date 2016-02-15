using UnityEngine;
using UnityEditor;
using AiBehaviour;

public class AiBehaviourWindow : EditorWindow {

    [SerializeField]
    private float _currentViewWidth = 200f;
    private Vector2 _scrollPosition = Vector2.zero;
    private bool _resize = false;
    private float _minimumViewWidth = 150f;
    private Rect _cursorChangeRect;

    private AiBlackboard _target;
    private StatusBarDrawer _statusBar;
    private ParamPanelDrawer _paramPanel;
    private TreeDrawer _treeDrawer;

    [MenuItem("Window/AiBehaviour")]
    public static void ShowEditor() {
        EditorWindow.GetWindow<AiBehaviourWindow>();
    }

    public void OnEnable() {
        titleContent = new GUIContent("AiBehaviour");
        titleContent.image = (Texture2D)EditorGUIUtility.Load("Assets/AiBehaviour/Icons/AiController.png");
        _statusBar = new StatusBarDrawer();
        _paramPanel = new ParamPanelDrawer();
        _cursorChangeRect = new Rect(_currentViewWidth, 0f, 5f, position.height);
    }

    private void OnGUI() {
        _statusBar.DrawStatusBar();
        ResizeSplitPanel();
        _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, GUILayout.Width(_currentViewWidth), GUILayout.Height(position.height));
        _paramPanel.DrawPanel();
        GUILayout.EndScrollView();
        GUI.BeginGroup(new Rect(_currentViewWidth, EditorStyles.toolbar.fixedHeight, position.width - _currentViewWidth, position.height), string.Empty, "AnimationCurveEditorBackground");
        BeginWindows();
        EditorUtils.DrawGrid(position);
        if (_treeDrawer != null) {
            _treeDrawer.DrawTree();
        }
        EndWindows();
        GUI.EndGroup();
        InputHandler();
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

    private void OnSelectionChange() {
        Init();
    }

    private void OnFocus() {
        Init();
    }

    private void OnProjectChange() {
        Init();
    }

    private void InputHandler() {
        int controlID = GUIUtility.GetControlID(new GUIContent("grid view"), FocusType.Passive);
        Event current = Event.current;
        if(current.button == 1 && current.type == EventType.MouseDown && _target != null) {
            var node = NodeFactory.CreateNode(NodeFactory.Nodes.IntNode, _target);
            node.Position = new Vector2(current.mousePosition.x - _currentViewWidth - NodeDrawer.gSize.x / 2, current.mousePosition.y - NodeDrawer.gSize.y);
            _statusBar.CurrentTree.AddNode(node);
            _treeDrawer = new TreeDrawer(_statusBar.CurrentTree);
            return;
        }
        if (current.button != 2 && (current.button != 0 || !current.alt)) {
            return;
        }
        switch (current.GetTypeForControl(controlID)) {
            case EventType.MouseDown:
                GUIUtility.hotControl = controlID;
                current.Use();
                EditorGUIUtility.SetWantsMouseJumping(1);
                break;
            case EventType.MouseUp:
                if (GUIUtility.hotControl == controlID) {
                    GUIUtility.hotControl = 0;
                    current.Use();
                    EditorGUIUtility.SetWantsMouseJumping(0);
                }
                break;
            case EventType.MouseMove:
            case EventType.MouseDrag:
                if (GUIUtility.hotControl == controlID) {
                    if(_treeDrawer != null) {
                        _treeDrawer.OffsetNodes(current.delta);
                    }
                    current.Use();
                }
                break;
        }
    }

    private void Init() {
        if (_target != Selection.activeObject && Selection.activeObject is AiBlackboard && EditorUtility.IsPersistent(Selection.activeObject)) {
            _target = (AiBlackboard)Selection.activeObject;
            _statusBar.Blackboard = _target;
            _paramPanel.Blackboard = _target;
            _treeDrawer = new TreeDrawer(_statusBar.CurrentTree);
            _statusBar.OnSelectedAiTree += _treeDrawer.RebuildTreeView;
        } else if(Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<AiController>() != null && Selection.activeGameObject.GetComponent<AiController>().Blackboard != null && Selection.activeGameObject.GetComponent<AiController>().Blackboard != _target) {
            _target = Selection.activeGameObject.GetComponent<AiController>().Blackboard;
            _statusBar.Blackboard = _target;
            _paramPanel.Blackboard = _target;
            _treeDrawer = new TreeDrawer(_statusBar.CurrentTree);
            _statusBar.OnSelectedAiTree += _treeDrawer.RebuildTreeView;
        }
        Repaint();
    }
}
