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
                if (_nodes.Contains(value)) {
                    _root = value;
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
                return true;
            }
            return false;
        }

        public bool Run() {
            return Root.Run();
        }
    }
}
