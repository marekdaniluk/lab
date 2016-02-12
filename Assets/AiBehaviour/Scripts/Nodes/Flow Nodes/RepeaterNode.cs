using UnityEngine;

namespace AiBehaviour {
    [System.Serializable]
    public class RepeaterNode : AFlowNode {

        [SerializeField]
        private int _repeat = 1;
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

        public int Repeat {
            get { return _repeat; }
            set { _repeat = value; }
        }

        public override bool Run() {
            for (int i = 0; i < Repeat; ++i) {
                _node.Run();
            }
            return true;
        }

        public override string ToString() {
            return string.Format("Repeater\n{0}?", _node.ToString());
        }
    }
}
