using UnityEngine;
using UnityEditor;
using lab;

public class NodeDrawer {

    public delegate void NodeDrawerHandler(ANode node);
    public static NodeDrawerHandler OnRightClicked = delegate { };
    public static NodeDrawerHandler OnLeftClicked = delegate { };

    public static readonly Vector2 gSize = new Vector2(144f, 32f);

    private static int gNodeDrawerIdIncremental = 0;

    private readonly string _rootStyle = "flow node 5";
    private readonly string _rootStyleOn = "flow node 5 on";
    private readonly string _defaultStyle = "flow node 0";
    private readonly string _defaultStyleOn = "flow node 0 on";

    private ANode _node;
    private int _id;
    private Rect _rect;
    private bool _isRoot = false;
	private GUIStyle _labelStyle;

	public ANode Node {
		get { return _node; }
	}

    public bool IsRoot {
        get { return _isRoot; }
        set { _isRoot = value; }
    }

    public void SetOffset(Vector2 offset) {
        _node.Position += offset;
    }

    public NodeDrawer(ANode node, bool isRoot = false) {
        _id = gNodeDrawerIdIncremental++;
        _node = node;
        _isRoot = isRoot;
        _rect = new Rect(0f, 0f, gSize.x, gSize.y);
		_labelStyle = new GUIStyle();
		_labelStyle.normal.textColor = Color.black;
    }

	public void ResetDebugInfo() {
		_labelStyle.normal.textColor = Color.black;
	}

	public void SetDebugInfo(bool result) {
		_labelStyle.normal.textColor = result ? new Color(0f, 0.5f, 0f) : Color.red;
	}

    public void DrawNode() {
        _rect.x = _node.Position.x;
        _rect.y = _node.Position.y;
        _rect = GUI.Window(_id, _rect, new GUI.WindowFunction(DrawNodeWindow), "", (Selection.activeObject != _node) ? (_isRoot ? _rootStyle : _defaultStyle) : (_isRoot ? _rootStyleOn : _defaultStyleOn));
        _node.Position.x = _rect.x;
        _node.Position.y = _rect.y;
    }

    private void DrawNodeWindow(int id) {
        Event e = Event.current;
        if (_node == null) {
            return;
        }
		GUI.Label(new Rect(0, gSize.y / 2f - 12, _rect.width, 24), new GUIContent(_node.GetType().Name, EditorGUIUtility.ObjectContent(_node, _node.GetType()).image), _labelStyle);
        GUI.Label(new Rect(gSize.x - 14f, -1f, 16f, 16f), new GUIContent((Texture2D)EditorGUIUtility.Load("Assets/cicik.png")));
        if (e.type == EventType.MouseUp && e.button == 1) {
            OnRightClicked(_node);
            e.Use();
        }
        if (e.type == EventType.MouseDown && e.button == 0) {
            OnLeftClicked(_node);
        }
        EditorUtility.SetDirty(_node);
        GUI.DragWindow();
    }
}
