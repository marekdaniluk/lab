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

        public override bool Run(ParameterContainer parameters, IList<AiTree> trees, List<ATaskScript> tasks) {
            return !_node.Run(parameters, trees, tasks);
        }
#if UNITY_EDITOR
        public override bool DebugRun(ParameterContainer parameters, IList<AiTree> trees, int level, int nodeIndex) {
            var result = _node.DebugRun(parameters, trees, (level + 1), 0);
			Debug.Log(string.Format("{0}{1}. Inverter Node. Result: <b><color={2}>{3}</color></b>", new string('\t', level), nodeIndex, result == false ? "green" : "red", !result));
			OnDebugResult(this, !result);
            return !result;
        }
#endif
    }
}
