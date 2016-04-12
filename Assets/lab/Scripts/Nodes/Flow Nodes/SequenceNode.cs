using UnityEngine;
using System.Collections.Generic;

namespace lab {
    /// <summary>
    /// One of the most important flow nodes. If one of child nodes returns false after run, SequenceNode also will return false.
    /// <para>SequenceNode aggregates child nodes and runs them from first to last. It stops running after first child node run result that is false, that means not all child nodes must be invoked.
    /// If all child nodes returns true, this node will also return true as a run result.</para>
    /// </summary>
    [System.Serializable]
    public class SequenceNode : AFlowNode {

        [SerializeField]
        private List<ANode> _nodes = new List<ANode>();

        /// <summary>
        /// Adds new node as a child.
        /// </summary>
        /// <param name="node">Node to be added as child.</param>
        /// <returns>True if adding succeed. Otherwise false.</returns>
        public override bool AddNode(ANode node) {
            if (_nodes.Contains(node)) {
                return false;
            }
            _nodes.Add(node);
            return true;
        }

        /// <summary>
        /// Removes child node.
        /// </summary>
        /// <param name="node">Child node to remove.</param>
        /// <returns>True if removing succeed. Otherwise false.</returns>
        public override bool RemoveNode(ANode node) {
            return _nodes.Remove(node);
        }

        /// <summary>
        /// Gets child node at index.
        /// </summary>
        /// <param name="i">Index of child node to get.</param>
        /// <returns>Child node under i index.</returns>
        public override ANode GetNode(int i) {
            return _nodes[i];
        }

        /// <summary>
        /// Gets count of child nodes.
        /// </summary>
        public override int NodeCount {
            get { return _nodes.Count; }
        }

        /// <summary>
        /// Runs this node.
        /// </summary>
        /// <param name="parameters">AiBlackboard with global parameters.</param>
        /// <param name="trees">Readonly list with all ai trees.</param>
        /// <param name="tasks">List of task scripts to bind with task nodes.</param>
        /// <returns>True if all child nodes succeed. Otherwise false.</returns>
        public override bool Run(AiBlackboard parameters, IList<AiTree> trees, List<ATaskScript> tasks) {
            for (int i = 0; i < _nodes.Count; ++i) {
                if (!_nodes[i].Run(parameters, trees, tasks)) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Runs debug this node.
        /// </summary>
        /// <param name="parameters">AiBlackboard with global parameters.</param>
        /// <param name="trees">Readonly list with all ai trees.</param>
        /// <returns>True if debug run succeed. Otherwise false.</returns>
        public override bool DebugRun(AiBlackboard parameters, IList<AiTree> trees) {
            for (int i = 0; i < _nodes.Count; ++i) {
                if (!_nodes[i].DebugRun(parameters, trees)) {
                    OnDebugResult(this, false);
                    return false;
                }
            }
            OnDebugResult(this, true);
            return true;
        }
    }
}
