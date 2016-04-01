using UnityEngine;
using System.Collections.Generic;

namespace lab {
	[System.Serializable]
	public class SucceederNode : AFlowNode {
		
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
		
		public override bool Run(ParameterContainer parameters, List<ATaskScript> tasks) {
			_node.Run(parameters, tasks);
			return true;
        }
#if UNITY_EDITOR
        public override bool DebugRun(int level, int nodeIndex) {
            _node.DebugRun((level + 1), 0);
			Debug.Log(string.Format("{0}{1}. Succeeder Node. Result: <b><color=green>true</color></b>", new string('\t', level), nodeIndex));
			OnDebugResult(this, true);
            return true;
        }
#endif
    }
}
