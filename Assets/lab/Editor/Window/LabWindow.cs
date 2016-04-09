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

    public static AiBehaviour _target;
    private StatusBarDrawer _statusBar;
    private ParamPanelDrawer _paramPanel;
    private TreeDrawer _treeDrawer;

	[MenuItem("Window/Lab Window")]
    public static void ShowEditor() {
        gWindow = EditorWindow.GetWindow<LabWindow>();
    }

    private void OnEnable() {
        titleContent = new GUIContent("lab");
		titleContent.image = (Texture2D)EditorGUIUtility.Load("Assets/lab/Icons/32x32/lab.png");
		Init();
		gWindow = this;
    }

	private void OnGUI() {
		if(_statusBar == null) {
            return;
		}
        _statusBar.DrawStatusBar();
        //a bit dirty hack of drawing buttons in param panel without making dependencies. Well, editor gui...
        EditorGUILayout.BeginHorizontal();
        if (_target != null && GUILayout.Button(new GUIContent((Texture2D)EditorGUIUtility.Load("Assets/lab/Icons/32x32/debug.png"), "run debug info"), "CommandLeft")) {
            _treeDrawer.RunDebug();
        }
        if (_target != null && GUILayout.Button(new GUIContent((Texture2D)EditorGUIUtility.Load("Assets/lab/Icons/32x32/reset.png"), "reset debug info"), "CommandMid")) {
            _treeDrawer.ResetDebug();
        }
        if (_target != null && GUILayout.Button(new GUIContent((Texture2D)EditorGUIUtility.Load("Assets/lab/Icons/32x32/center.png"), "center view on root"), "CommandRight")) {
            var pos = -_statusBar.CurrentTree.Root.Position + (new Vector2(position.width - _currentViewWidth, position.height - EditorStyles.toolbar.fixedHeight) - NodeDrawer.gSize) / 2;
            pos.x = Mathf.Floor(pos.x);
            pos.y = Mathf.Floor(pos.y);
            _treeDrawer.OffsetNodes(pos);
        }
        EditorGUILayout.EndHorizontal();
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
        if (Selection.activeObject == _target) {
            return;
        }
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
        if (Selection.activeObject is AiBehaviour && EditorUtility.IsPersistent(Selection.activeObject)) {
            _target = (AiBehaviour)Selection.activeObject;
            _statusBar.Blackboard = _target;
            _paramPanel.Blackboard = _target;
            _treeDrawer = new TreeDrawer(_statusBar.CurrentTree);
            _statusBar.OnSelectedAiTree += _treeDrawer.RebuildTreeView;
        } else if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<AiController>() != null && Selection.activeGameObject.GetComponent<AiController>().Behaviour != null) {
            _target = Selection.activeGameObject.GetComponent<AiController>().Behaviour;
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
            if (_statusBar.CurrentTree.AddNode(node) && _statusBar.CurrentTree.Root == null) {
                _statusBar.CurrentTree.Root = node;
            }
            _treeDrawer.RebuildTreeView(_statusBar.CurrentTree);
        }
    }
}
