using UnityEngine;
using System.Collections.Generic;

namespace AiBehaviour {
    [System.Serializable]
    public class AiTree {

        [SerializeField]
        private ANode _root;
        [SerializeField]
        private List<ANode> _nodes;

        public ANode Root {
            get { return _root; }
            set {
                if(_nodes != null && _nodes.Contains(value)) {
                    _root = value;
                }
            }
        }

        public bool AddNode(ANode node) {
            if(_nodes == null) {
                _nodes = new List<ANode>();
            } else if (_nodes.Contains(node)) {
                return false;
            }
            _nodes.Add(node);
            if (_nodes.Count == 1) {
                Root = node;
            }
            return true;
        }

        public bool RemoveNode(ANode node) {
            return _nodes.Remove(node);
        }

        public bool Run() {
            return Root.Run();
        }
    }
}
