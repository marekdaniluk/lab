using UnityEngine;
using System.Collections.Generic;

namespace lab {
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

        public override bool Run(List<ATaskScript> tasks) {
            for (int i = 0; i < Repeat; ++i) {
                _node.Run(tasks);
            }
            return true;
        }
#if UNITY_EDITOR
        public override bool DebugRun(int level, int nodeIndex) {
            for (int i = 0; i < Repeat; ++i) {
                _node.DebugRun((level + 1), 0);
            }
            Debug.Log(string.Format("{0}{1}. Repeater Node. Result: <b><color=green>true</color></b>", new string('\t', level), nodeIndex));
            return true;
        }
#endif
    }
}
