using UnityEngine;
using System.Collections.Generic;

namespace lab {
    /// <summary>
    /// Node that gives possibility to run other AiTree.
    /// <para>AiBehaviour is a container for many trees, it is possible to have multiple trees for one behaviour and run them in different circumstances.
    /// AiTree gives another oppotrunity, to make AiTree more readable by dividing them on smaller chunks and running by this node.</para>
    /// </summary>
    [System.Serializable]
    public class TreeNode : ANode {

        [SerializeField]
        private int _treeIndex = 0;

        /// <summary>
        /// Sets/Gets index of AiTree to run.
        /// </summary>
        public int TreeIndex {
            get { return _treeIndex; }
            set { _treeIndex = value; }
        }

        /// <summary>
        /// Runs this node.
        /// </summary>
        /// <param name="parameters">AiBlackboard with global parameters.</param>
        /// <param name="trees">Readonly list with all ai trees.</param>
        /// <param name="tasks">List of task scripts to bind with task nodes.</param>
        /// <returns>True if node succeed. Otherwise false.</returns>
        public override bool Run(AiBlackboard parameters, IList<AiTree> trees, List<ATaskScript> tasks) {
            return trees[TreeIndex].Run(parameters, trees, tasks);
        }

        /// <summary>
        /// Runs debug this node.
        /// </summary>
        /// <param name="parameters">AiBlackboard with global parameters.</param>
        /// <param name="trees">Readonly list with all ai trees.</param>
        /// <param name="level">Level of how deep we are in this AiTree.</param>
        /// <param name="nodeIndex">Index of current node in parent's node. If this is root, nodeIndex is 0.</param>
        /// <returns>True if debug run succeed. Otherwise false.</returns>
        public override bool DebugRun(AiBlackboard parameters, IList<AiTree> trees, int level, int nodeIndex) {
            var result = trees[TreeIndex].DebugRun(parameters, trees, (level + 1));
            Debug.Log(string.Format("{0}{1}. Tree Node. Result: <b><color={2}>{3}</color></b>", new string('\t', level), nodeIndex, result ? "green" : "red", result));
            OnDebugResult(this, result);
            return true;
        }
    }
}
