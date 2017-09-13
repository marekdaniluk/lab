using UnityEngine;
using UnityEditor;
using lab;
using lab.EditorView;
using System;

public class LabWindow : EditorWindow {

    [SerializeField]
    private bool _resize = false;
    private float _minimumViewWidth = 150f;
    private Rect _cursorChangeRect;

    public static AiBehaviour _target;
    private StatusBarDrawer _statusBar;
    private TreeDrawer _treeDrawer;

	[MenuItem("Window/Lab Window")]
    public static void ShowEditor() {
        var window = EditorWindow.GetWindow<LabWindow>();
        window.titleContent = new GUIContent("lab");
        window.titleContent.image = (Texture2D)EditorGUIUtility.Load("Assets/lab/Icons/32x32/lab.png");
        window.Show();
    }

    private void OnEnable() {
		Init();
        SequenceNodeEditor.OnSequenceNodeChanged += Repaint;
        SelectorNodeEditor.OnSelectorNodeChanged += Repaint;
        RepeaterNodeEditor.OnRepeaterNodeChanged += Repaint;
        InverterNodeEditor.OnInverterNodeChanged += Repaint;
        NodeDrawer.OnDuplicate += DuplicateNode;
    }

    private void OnDisable() {
        SequenceNodeEditor.OnSequenceNodeChanged -= Repaint;
        SelectorNodeEditor.OnSelectorNodeChanged -= Repaint;
        RepeaterNodeEditor.OnRepeaterNodeChanged -= Repaint;
        InverterNodeEditor.OnInverterNodeChanged -= Repaint;
        NodeDrawer.OnDuplicate -= DuplicateNode;
    }

    private void DuplicateNode(ANode obj) {
        var node = obj.Clone();
        NodeFactory.AddNodeToTarget(node, _target);
        if(_statusBar.CurrentTree.AddNode(node) && _statusBar.CurrentTree.Root == null) {
            _statusBar.CurrentTree.Root = node;
        }
        _treeDrawer.RebuildTreeView(_statusBar.CurrentTree);
        Repaint();
    }

    private void OnGUI() {
		if(_statusBar == null) {
            return;
		}
        _statusBar.DrawStatusBar();
        GUI.BeginGroup(new Rect(0f, EditorStyles.toolbar.fixedHeight, position.width, position.height), string.Empty);
        BeginWindows();
        EditorUtils.DrawGrid(position);
        if (_treeDrawer != null) {
            _treeDrawer.DrawTree();
        }
        EndWindows();
        GUI.EndGroup();
        EditorGUILayout.BeginHorizontal();
        GUI.enabled = _statusBar.CurrentTree != null && _statusBar.CurrentTree.Root != null;
        if(_target != null && GUILayout.Button(new GUIContent((Texture2D)EditorGUIUtility.Load("Assets/lab/Icons/32x32/debug.png"),"run debug info"),"CommandLeft")) {
            _treeDrawer.ResetDebug();
            _statusBar.CurrentTree.DebugRun(_target.Blackboard,_target.Trees);
        }
        GUI.enabled = true;
        if(_target != null && GUILayout.Button(new GUIContent((Texture2D)EditorGUIUtility.Load("Assets/lab/Icons/32x32/reset.png"),"reset debug info"),"CommandMid")) {
            _treeDrawer.ResetDebug();
        }
        GUI.enabled = _statusBar.CurrentTree != null && _statusBar.CurrentTree.Nodes.Count > 0;
        if(_target != null && GUILayout.Button(new GUIContent((Texture2D)EditorGUIUtility.Load("Assets/lab/Icons/32x32/center.png"),"center view on root"), "CommandRight")) {
            var nodePos = _statusBar.CurrentTree.Root != null ? _statusBar.CurrentTree.Root.Position : _statusBar.CurrentTree.Nodes[0].Position;
            var pos = -nodePos + (new Vector2(position.width,position.height - EditorStyles.toolbar.fixedHeight) - NodeDrawer.gSize) / 2;
            pos.x = Mathf.Floor(pos.x);
            pos.y = Mathf.Floor(pos.y);
            _treeDrawer.OffsetNodes(pos);
        }
        GUI.enabled = true;
        EditorGUILayout.EndHorizontal();
        InputHandler();
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
        }
        if (_target == null && _target is AiBehaviour) {
            Init();
        }
    }

    private void InputHandler() {
		if(_target == null) {
            return;
        }
        int controlID = GUIUtility.GetControlID(new GUIContent("grid view"),FocusType.Passive);
        Event current = Event.current;
        if ((current.type == EventType.MouseDown || current.type == EventType.MouseUp)) {
            Selection.activeObject = _target;
            Repaint();
        }
        if (current.button == 1 && current.type == EventType.MouseDown && _target != null) {
            NodeFactory.CreateNodeMenu(current.mousePosition, MenuCallback);
            current.Use();
            return;
        }
        if (current.button != 2) {
            return;
        }
        switch(current.GetTypeForControl(controlID)) {
            case EventType.MouseDown:
                GUIUtility.hotControl = controlID;
                current.Use();
                EditorGUIUtility.SetWantsMouseJumping(1);
                break;
            case EventType.MouseUp:
                if(GUIUtility.hotControl == controlID) {
                    GUIUtility.hotControl = 0;
                    current.Use();
                    EditorGUIUtility.SetWantsMouseJumping(0);
                }
                break;
            case EventType.MouseMove:
            case EventType.MouseDrag:
                if(GUIUtility.hotControl == controlID) {
                    if(_treeDrawer != null) {
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
        _cursorChangeRect = new Rect(0f, 0f, 5f, position.height);
        if (Selection.activeObject is AiBehaviour && EditorUtility.IsPersistent(Selection.activeObject)) {
            _target = (AiBehaviour)Selection.activeObject;
            _statusBar.Blackboard = _target;
            _treeDrawer = new TreeDrawer(_statusBar.CurrentTree);
            _statusBar.OnSelectedAiTree += _treeDrawer.RebuildTreeView;
        } else if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<AiController>() != null && Selection.activeGameObject.GetComponent<AiController>().Behaviour != null) {
            _target = Selection.activeGameObject.GetComponent<AiController>().Behaviour;
            _statusBar.Blackboard = _target;
            _treeDrawer = new TreeDrawer(_statusBar.CurrentTree);
            _statusBar.OnSelectedAiTree += _treeDrawer.RebuildTreeView;
        }
        Repaint();
    }

    private void MenuCallback(object obj) {
        var data = obj as NodeFactory.NodeCallbackData;
        if (data != null) {
            var node = NodeFactory.CreateNode(data.nodeType);
            node.Position = new Vector2(data.position.x - NodeDrawer.gSize.x / 2,data.position.y - NodeDrawer.gSize.y);
            NodeFactory.AddNodeToTarget(node,_target);
            if (_statusBar.CurrentTree.AddNode(node) && _statusBar.CurrentTree.Root == null) {
                _statusBar.CurrentTree.Root = node;
            }
            _treeDrawer.RebuildTreeView(_statusBar.CurrentTree);
        }
    }
}
