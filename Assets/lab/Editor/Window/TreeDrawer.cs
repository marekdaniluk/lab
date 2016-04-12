using UnityEngine;
using UnityEditor;
using lab;
using System.Collections.Generic;

public class TreeDrawer {

    private AiTree _tree;
	private List<NodeDrawer> _nodeDrawers;
	private AFlowNode _startConnection = null;

    public TreeDrawer(AiTree tree) {
        NodeDrawer.OnLeftClicked += NodeLeftClicked;
        NodeDrawer.OnRightClicked += ShowContextMenu;
        RebuildTreeView(tree);
    }

    public bool ForceRepaint() {
        return _startConnection != null;
    }

	public void ResetDebug() {
		EditorUtils.ClearConsole();
		if(_nodeDrawers == null) {
			return;
		}
		foreach(NodeDrawer drawer in _nodeDrawers) {
			drawer.ResetDebugInfo();
		}
	}

    public void DrawTree() {
        DrawTransition();
        if(_startConnection != null) {
            EditorUtils.DrawLine(new Vector3(_startConnection.Position.x + NodeDrawer.gSize.x / 2, _startConnection.Position.y + NodeDrawer.gSize.y / 2, 0), Event.current.mousePosition, Color.grey);
        }
        for (int i = 0; i < _nodeDrawers.Count; ++i) {
            _nodeDrawers[i].DrawNode();
        }
    }

    public void OffsetNodes(Vector2 offset) {
        for (int i = 0; i < _nodeDrawers.Count; ++i) {
            _nodeDrawers[i].SetOffset(offset);
        }
    }

    public void RebuildTreeView(AiTree tree) {
        _tree = tree;
        if (_tree == null) {
            return;
        }
		if(_nodeDrawers != null) {
			foreach(NodeDrawer drawer in _nodeDrawers) {
				drawer.Node.OnDebugResult -= DebugResult;
			}
		}
		_nodeDrawers = new List<NodeDrawer>();
		for (int i = 0; i < _tree.Nodes.Count; ++i) {
			_tree.Nodes[i].OnDebugResult += DebugResult;
            _nodeDrawers.Add(new NodeDrawer(_tree.Nodes[i], _tree.Root == _tree.Nodes[i]));
        }
    }

    private void DrawTransition() {
        var nodes = _tree.Nodes;
        for (int i = 0; i < nodes.Count; ++i) {
            var n = nodes[i] as AFlowNode;
            if (n != null) {
                for (int k = 0; k < n.NodeCount; ++k) {
                    Vector2 start = n.Position;
                    Vector2 end = n.GetNode(k).Position;
                    EditorUtils.DrawLabel(n.Position + new Vector2(NodeDrawer.gSize.x / 2, 0f) + (end - start).normalized * Vector2.Distance(start, end) / 2f, k.ToString());
                    EditorUtils.DrawNodeCurve(new Rect(n.Position.x, n.Position.y, NodeDrawer.gSize.x, NodeDrawer.gSize.y), new Rect(n.GetNode(k).Position.x, n.GetNode(k).Position.y, NodeDrawer.gSize.x, NodeDrawer.gSize.y));
                }
            }
        }
    }

    private void NodeLeftClicked(ANode node) {
        if (_tree == null || !_tree.Nodes.Contains(node)) {
            return;
        }
        Selection.activeObject = node;
        if(_startConnection != null) {
            if(_startConnection != node && _tree.ConnectNodes(_startConnection, node)) {
                AssetDatabase.SaveAssets();
            }
            _startConnection = null;
        }
    }

    private void ShowContextMenu(ANode node) {
        if (_tree == null || !_tree.Nodes.Contains(node)) {
            return;
        }
        GenericMenu menu = new GenericMenu();
        if(node is AFlowNode) {
			if(node != _tree.Root) {
				menu.AddItem(new GUIContent("Make Root"), false, RootCallback, node);
			} else {
				menu.AddDisabledItem(new GUIContent("Make Root"));
			}
            menu.AddItem(new GUIContent("Connect"), false, ConnectCallback, node);
        } else {
            menu.AddDisabledItem(new GUIContent("Make Root"));
            menu.AddDisabledItem(new GUIContent("Connect"));
        }
        menu.AddSeparator("");
        menu.AddItem(new GUIContent("Delete"), false, DeleteCallback, node);
        menu.ShowAsContext();
    }

    private void RootCallback(object obj) {
        var n = obj as ANode;
        if (n != null && n != _tree.Root) {
            _tree.Root = n;
            AssetDatabase.SaveAssets();
            RebuildTreeView(_tree);
        }
    }

    private void ConnectCallback(object obj) {
        var n = obj as AFlowNode;
        if (n != null) {
            _startConnection = n;
        }
    }

    private void DeleteCallback(object obj) {
        var n = obj as ANode;
        if (n != null && _tree.RemoveNode(n)) {
			n.OnDebugResult -= DebugResult;
            Selection.activeObject = _tree.Root;
            Object.DestroyImmediate(n, true);
            AssetDatabase.SaveAssets();
            RebuildTreeView(_tree);
        }
    }

	private void DebugResult(ANode node, bool result) {
		foreach(NodeDrawer drawer in _nodeDrawers) {
			if(drawer.Node == node) {
				drawer.SetDebugInfo(result);
				return;
			}
		}
	}
}
