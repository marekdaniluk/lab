using UnityEngine;
using System.Collections.Generic;

namespace lab {
    /// <summary>
    /// Flow node that always repeats running child node.
    /// <para>Another AFlowNode that has only one child. RepeaterNode repeats invoking child node and at the end always returns true as the result of run.</para>
    /// </summary>
    [System.Serializable]
    public class RepeaterNode : AFlowNode {

        [SerializeField]
        private int _repeat = 1;
        [SerializeField]
        private ANode _node;

        /// <summary>
        /// Adds new node as child. There can be only one child for RepeaterNode, so child will be overriden.
        /// </summary>
        /// <param name="node">Node to be added as a child.</param>
        /// <returns>Always returns true.</returns>
        public override bool AddNode(ANode node) {
            _node = node;
            return true;
        }

        /// <summary>
        /// Removes child node.
        /// </summary>
        /// <param name="node">Child node to be removed.</param>
        /// <returns>True if provided node to remove is the current child node. Otherwise false.</returns>
        public override bool RemoveNode(ANode node) {
            if (_node == node) {
                _node = null;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets child node.
        /// </summary>
        /// <param name="i">Index of child node to get. RepeaterNode has one child node, so only 0 will work.</param>
        /// <returns>Child node if i was 0 and child node was attached. Otherwise null.</returns>
        public override ANode GetNode(int i) {
            if (i == 0) {
                return _node;
            }
            return null;
        }

        /// <summary>
        /// Gets count of child nodes. RepeaterNode can give only two values, 0 or 1.
        /// </summary>
        public override int NodeCount {
            get { return (_node == null) ? 0 : 1; }
        }

        /// <summary>
        /// Gets/Sets number of repeats.
        /// </summary>
        public int Repeat {
            get { return _repeat; }
            set { _repeat = value; }
        }

        /// <summary>
        /// Runs this node.
        /// </summary>
        /// <param name="parameters">AiBlackboard with global parameters.</param>
        /// <param name="trees">Readonly list with all ai trees.</param>
        /// <param name="tasks">List of task scripts to bind with task nodes.</param>
        /// <returns>Always returns true.</returns>
        public override bool Run(AiBlackboard parameters, IList<AiTree> trees, List<TaskBinder> tasks) {
            for (int i = 0; i < Repeat; ++i) {
                _node.Run(parameters, trees, tasks);
            }
            return true;
        }


        /// <summary>
        /// Runs debug this node.
        /// </summary>
        /// <param name="parameters">AiBlackboard with global parameters.</param>
        /// <param name="trees">Readonly list with all ai trees.</param>
        /// <returns>Always returns true.</returns>
        public override bool DebugRun(AiBlackboard parameters, IList<AiTree> trees) {
            for (int i = 0; i < Repeat; ++i) {
                _node.DebugRun(parameters, trees);
            }
            OnDebugResult(this, true);
            return true;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() {
            return GetType().Name;
        }
    }
}
