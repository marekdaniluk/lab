using UnityEngine;
using System.Collections.Generic;

namespace AiBehaviour {
    [System.Serializable]
    public class SequenceNode : AFlowNode {

        [SerializeField]
        private List<ANode> _nodes;

        public override bool AddNode(ANode node) {
            if (_nodes == null) {
                _nodes = new List<ANode>();
            } else if (_nodes.Contains(node)) {
                return false;
            }
            _nodes.Add(node);
            return true;
        }

        public override bool RemoveNode(ANode node) {
            return _nodes.Remove(node);
        }

        public override ANode GetNode(int i) {
            return _nodes[i];
        }

        public override int NodeCount {
            get { return _nodes == null ? 0 : _nodes.Count; }
        }

        public override bool Run() {
            for (int i = 0; i < _nodes.Count; ++i) {
                if (!_nodes[i].Run()) {
                    return false;
                }
            }
            return true;
        }

        public override string ToString() {
            System.Text.StringBuilder builder = new System.Text.StringBuilder("Sequence\n");
            foreach (ANode node in _nodes) {
                builder.Append(string.Format("\t{0}\n", node.ToString()));
            }
            return builder.ToString();
        }
    }
}