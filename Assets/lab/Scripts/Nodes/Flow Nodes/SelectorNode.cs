using UnityEngine;
using System.Collections.Generic;

namespace lab {
    [System.Serializable]
    public class SelectorNode : AFlowNode {

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

        public override bool Run(Blackboard parameters, IList<AiTree> trees, List<ATaskScript> tasks) {
            for (int i = 0; i < _nodes.Count; ++i) {
                if (_nodes[i].Run(parameters, trees, tasks)) {
                    return true;
                }
            }
            return false;
        }
#if UNITY_EDITOR
        public override bool DebugRun(Blackboard parameters, IList<AiTree> trees, int level, int nodeIndex) {
            for (int i = 0; i < _nodes.Count; ++i) {
                if (_nodes[i].DebugRun(parameters, trees, (level + 1), i)) {
					Debug.Log(string.Format("{0}{1}. Selector Node. Result: <b><color=green>true</color></b>", new string('\t', level), nodeIndex));
					OnDebugResult(this, true);
                    return true;
                }
            }
			Debug.Log(string.Format("{0}{1}. Selector Node. Result: <b><color=red>false</color></b>", new string('\t', level), nodeIndex));
			OnDebugResult(this, false);
            return false;
        }
#endif
    }
}