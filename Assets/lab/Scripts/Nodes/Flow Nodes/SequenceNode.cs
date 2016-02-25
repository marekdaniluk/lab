using UnityEngine;
using System.Collections.Generic;

namespace lab {
    [System.Serializable]
    public class SequenceNode : AFlowNode {

        [SerializeField]
        private List<ANode> _nodes = new List<ANode>();

        public override bool AddNode(ANode node) {
            if (_nodes.Contains(node)) {
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
            get { return _nodes.Count; }
        }

        public override bool Run(List<ATaskScript> tasks) {
            for (int i = 0; i < _nodes.Count; ++i) {
                if (!_nodes[i].Run(tasks)) {
                    return false;
                }
            }
            return true;
        }
    }
}