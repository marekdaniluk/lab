using System.Collections.Generic;
using UnityEngine;

namespace lab {
    [System.Serializable]
    public class TreeNode : ABlackboardNode {

        [SerializeField]
        private int _treeIndex = 0;

        public int TreeIndex {
            get { return _treeIndex; }
            set { _treeIndex = value; }
        }

        public override bool Run(List<ATaskScript> tasks) {
            return Blackboard.Trees[TreeIndex].Run(tasks);
        }
#if UNITY_EDITOR
		public override bool DebugRun(int level, int nodeIndex) {
			var result = Blackboard.Trees[TreeIndex].DebugRun((level + 1));
			Debug.Log(string.Format("{0}{1}. Tree Node. Result: <b><color={2}>{3}</color></b>", new string('\t', level), nodeIndex, result ? "green" : "red", result));
			OnDebugResult(this, result);
            return true;
        }
#endif
    }
}
