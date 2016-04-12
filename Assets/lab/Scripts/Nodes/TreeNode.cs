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
        /// <returns>True if debug run succeed. Otherwise false.</returns>
        public override bool DebugRun(AiBlackboard parameters, IList<AiTree> trees) {
            var result = trees[TreeIndex].DebugRun(parameters, trees);
            OnDebugResult(this, result);
            return true;
        }
    }
}
