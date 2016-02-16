using UnityEngine;
using System.Collections.Generic;

namespace AiBehaviour {
    [System.Serializable]
    public class AiTree {

        [SerializeField]
        private ANode _root;
        [SerializeField]
        private List<ANode> _nodes = new List<ANode>();

        public ANode Root {
            get { return _root; }
            set {
                var node = value as AFlowNode;
                if (_nodes.Contains(node) && node != null) {
                    //fix connections between new root node and other nodes
                    for (int i = 0; i < _nodes.Count; ++i) {
                        var n = _nodes[i] as AFlowNode;
                        if (n != null) {
                            n.RemoveNode(value);
                        }
                    }
                    _root = value;
                } else {
                    _root = null;
                }
            }
        }

        public List<ANode> Nodes {
            get { return _nodes; }
        }

        public bool AddNode(ANode node) {
            if (_nodes.Contains(node)) {
                return false;
            }
            _nodes.Add(node);
            if (_nodes.Count == 1) {
                Root = node;
            }
            return true;
        }

        public bool RemoveNode(ANode node) {
            if (_nodes.Remove(node)) {
                if (Root == node && _nodes.Count > 0) {
                    Root = _nodes[0];
                }
                //remove connections to removed node
                for (int i = 0; i < _nodes.Count; ++i) {
                    var n = _nodes[i] as AFlowNode;
                    if (n != null) {
                        for (int j = 0; j < n.NodeCount; ++j) {
                            if (n.GetNode(j) == node) {
                                n.RemoveNode(node);
                            }
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public bool ConnectNodes(AFlowNode from, ANode to) {
            if (_nodes.Contains(from) && _nodes.Contains(to) && to != Root) {
                var n = to as AFlowNode;
                if (n != null) {
                    //check nodes recursive to prevent circular trees
                    if (IsConnected(from, n)) {
                        return false;
                    }
                }
                return from.AddNode(to);
            }
            return false;
        }

        public bool Run() {
            return Root.Run();
        }

        private bool IsConnected(AFlowNode from, AFlowNode to) {
            for (int i = 0; i < to.NodeCount; ++i) {
                var n = to.GetNode(i) as AFlowNode;
                if (n != null) {
                    if (from == n) {
                        return true;
                    }
                    if (IsConnected(from, n)) {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
