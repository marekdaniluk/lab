using UnityEngine;
using UnityEditor;
using AiBehaviour;
using System.Collections.Generic;

public class TreeDrawer {

    private AiTree _tree;
    private List<NodeDrawer> _nodeDrawers;

    public TreeDrawer(AiTree tree) {
        RebuildTreeView(tree);
    }

    public void DrawTree() {
        DrawTransition(_tree.Root);
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
        _nodeDrawers = new List<NodeDrawer>();
        for (int i = 0; i < _tree.Nodes.Count; ++i) {
            _nodeDrawers.Add(new NodeDrawer(_tree.Nodes[i], _tree.Root == _tree.Nodes[i]));
        }
    }

    private void DrawTransition(ANode node) {
        var n = node as AFlowNode;
        if (n != null) {
            for(int i = 0; i < n.NodeCount; ++i) {
                EditorUtils.DrawNodeCurve(new Rect(n.Position.x, n.Position.y, NodeDrawer.gSize.x, NodeDrawer.gSize.y), new Rect(n.GetNode(i).Position.x, n.GetNode(i).Position.y, NodeDrawer.gSize.x, NodeDrawer.gSize.y));
                DrawTransition(n.GetNode(i));
            }
        }
    }
}
