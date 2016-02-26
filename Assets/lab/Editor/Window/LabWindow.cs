using UnityEngine;
using UnityEditor;
using lab;

public class LabWindow : EditorWindow {

    public static LabWindow gWindow = null;

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

	[MenuItem("Window/Lab Window")]
    public static void ShowEditor() {
        gWindow = EditorWindow.GetWindow<LabWindow>();
        gWindow.Init();
    }

    private void OnEnable() {
		titleContent = new GUIContent("lab");
		titleContent.image = (Texture2D)EditorGUIUtility.Load("Assets/lab/Icons/icon_lab1.png");
    }

    private void OnDisable() {
        gWindow = null;
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
		if(_target != null && GUI.Button(new Rect(position.width - _currentViewWidth - 55f, position.height - EditorStyles.toolbar.fixedHeight - 55f, 50f, 50f), "C")) {
            var pos = -_statusBar.CurrentTree.Root.Position + (new Vector2(position.width - _currentViewWidth, position.height - EditorStyles.toolbar.fixedHeight) - NodeDrawer.gSize) / 2;
            pos.x = Mathf.Floor(pos.x);
            pos.y = Mathf.Floor(pos.y);
			_treeDrawer.OffsetNodes(pos);
		}
        GUI.EndGroup();
        InputHandler();
    }

    private void Update() {
        if (_treeDrawer != null && _treeDrawer.ForceRepaint()) {
            Repaint();
        }
    }

    private void ResizeSplitPanel() {
        EditorGUIUtility.AddCursorRect(_cursorChangeRect, MouseCursor.ResizeHorizontal);
        if (Event.current.type == EventType.mouseDown && _cursorChangeRect.Contains(Event.current.mousePosition)) {
            _resize = true;
        }
        if (_resize) {
            _currentViewWidth = Mathf.Clamp(Event.current.mousePosition.x, _minimumViewWidth, position.width - _minimumViewWidth);
            _cursorChangeRect.Set(_currentViewWidth - 5f, 0f, 5f, position.height);
            Repaint();
        }
        if (Event.current.type == EventType.MouseUp) {
            _resize = false;
        }
    }

    private void OnSelectionChange() {
        Init();
    }

    private void OnProjectChange() {
        if (Selection.activeObject == null && Selection.activeGameObject == null) {
            _treeDrawer = null;
            _target = null;
            _statusBar.Blackboard = null;
            _paramPanel.Blackboard = null;
        }
        if (_target == null) {
            Init();
        }
    }

    private void InputHandler() {
		if(_target == null) {
			return;
		}
        int controlID = GUIUtility.GetControlID(new GUIContent("grid view"), FocusType.Passive);
        Event current = Event.current;
        Rect r = new Rect(0f, 0f, _currentViewWidth + 1f, position.height);
        if ((current.type == EventType.MouseDown || current.type == EventType.MouseUp)) {
            Selection.activeObject = _target;
            if (r.Contains(current.mousePosition)) {
                return;
            }
        }
        if (current.button == 1 && current.type == EventType.MouseDown && _target != null) {
            NodeFactory.CreateNodeMenu(current.mousePosition, MenuCallback);
            current.Use();
            return;
        }
        if (current.button != 2) {
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
                    if (_treeDrawer != null) {
                        _treeDrawer.OffsetNodes(current.delta);
                    }
                    current.Use();
                }
                break;
        }
    }

    private void Init() {
        if (_statusBar == null) {
            _statusBar = new StatusBarDrawer();
        }
        if (_paramPanel == null) {
            _paramPanel = new ParamPanelDrawer();
        }
        _cursorChangeRect = new Rect(_currentViewWidth, 0f, 5f, position.height);
        if (_target != Selection.activeObject && Selection.activeObject is AiBlackboard && EditorUtility.IsPersistent(Selection.activeObject)) {
            _target = (AiBlackboard)Selection.activeObject;
            _statusBar.Blackboard = _target;
            _paramPanel.Blackboard = _target;
            _treeDrawer = new TreeDrawer(_statusBar.CurrentTree);
            _statusBar.OnSelectedAiTree += _treeDrawer.RebuildTreeView;
        } else if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<AiController>() != null && Selection.activeGameObject.GetComponent<AiController>().Blackboard != null && Selection.activeGameObject.GetComponent<AiController>().Blackboard != _target) {
            _target = Selection.activeGameObject.GetComponent<AiController>().Blackboard;
            _statusBar.Blackboard = _target;
            _paramPanel.Blackboard = _target;
            _treeDrawer = new TreeDrawer(_statusBar.CurrentTree);
            _statusBar.OnSelectedAiTree += _treeDrawer.RebuildTreeView;
        }
        Repaint();
    }

    private void MenuCallback(object obj) {
        var data = obj as NodeFactory.NodeCallbackData;
        if (data != null) {
            var node = NodeFactory.CreateNode(data.nodeType, _target);
            node.Position = new Vector2(data.position.x - _currentViewWidth - NodeDrawer.gSize.x / 2, data.position.y - NodeDrawer.gSize.y);
            _statusBar.CurrentTree.AddNode(node);
            _treeDrawer.RebuildTreeView(_statusBar.CurrentTree);
        }
    }
}
