using System.Collections.Generic;
using UnityEngine;

namespace lab {
    [System.Serializable]
    public class TreeNode : ANode {

        [SerializeField]
        private int _treeIndex = 0;

        public int TreeIndex {
            get { return _treeIndex; }
            set { _treeIndex = value; }
        }

        public override bool Run(ParameterContainer parameters, IList<AiTree> trees, List<ATaskScript> tasks) {
            return trees[TreeIndex].Run(parameters, trees, tasks);
        }
#if UNITY_EDITOR
		public override bool DebugRun(ParameterContainer parameters, IList<AiTree> trees, int level, int nodeIndex) {
            var result = trees[TreeIndex].DebugRun(parameters, trees, (level + 1));
			Debug.Log(string.Format("{0}{1}. Tree Node. Result: <b><color={2}>{3}</color></b>", new string('\t', level), nodeIndex, result ? "green" : "red", result));
			OnDebugResult(this, result);
            return true;
        }
#endif
    }
}
