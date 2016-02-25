using UnityEngine;
using System.Collections.Generic;

namespace lab {
    [System.Serializable]
    public class InverterNode : AFlowNode {

        [SerializeField]
        private ANode _node;

        public override bool AddNode(ANode node) {
            _node = node;
            return true;
        }

        public override bool RemoveNode(ANode node) {
            if (_node == node) {
                _node = null;
                return true;
            }
            return false;
        }

        public override ANode GetNode(int i) {
            if (i == 0) {
                return _node;
            }
            return null;
        }

        public override int NodeCount {
            get { return (_node == null) ? 0 : 1; }
        }

        public override bool Run(List<ATaskScript> tasks) {
            return !_node.Run(tasks);
        }
    }
}
