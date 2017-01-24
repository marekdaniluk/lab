using UnityEngine;
using System.Collections.Generic;

namespace lab {
    /// <summary>
    /// Behaviour tree with ai logic.
    /// <para>Single ai tree that can be run.</para>
    /// </summary>
    [System.Serializable]
    public class AiTree {

        [SerializeField]
        private ANode _root;
        [SerializeField]
        private List<ANode> _nodes = new List<ANode>();

        /// <summary>
        /// Sets/Gets root node for current AiTree. Only flow nodes can be root nodes.
        /// </summary>
        public ANode Root {
            get { return _root; }
            set {
                var node = value as AFlowNode;
                if (node != null && _nodes.Contains(node)) {
                    //fix connections between new root node and other nodes
                    for (int i = 0; i < _nodes.Count; ++i) {
                        var n = _nodes[i] as AFlowNode;
                        if (n != null) {
                            n.RemoveNode(node);
                        }
                    }
                    _root = node;
                } else {
                    _root = null;
                }
            }
        }

        /// <summary>
        /// Gets readonly list of all nodes assigned to this AiTree.
        /// </summary>
        public IList<ANode> Nodes {
            get { return _nodes.AsReadOnly(); }
        }

        /// <summary>
        /// Adds new node to this AiTree.
        /// </summary>
        /// <param name="node">A node to add to this AiTree.</param>
        /// <returns>True if adding node was succeed. Otherwise false.</returns>
        public bool AddNode(ANode node) {
            if (_nodes.Contains(node)) {
                return false;
            }
            _nodes.Add(node);
            return true;
        }

        /// <summary>
        /// Removes node from this AiTree. Also removes all connections between other nodes, so it doesn't have to be done manually.
        /// </summary>
        /// <param name="node">A node to remove from this AiTree.</param>
        /// <returns>True if removing node was succeed. Otherwise false.</returns>
        public bool RemoveNode(ANode node) {
            if (_nodes.Remove(node)) {
                if (Root == node) {
                    Root = null;
                }
                //remove connections to removed node
                for (int i = 0; i < _nodes.Count; ++i) {
                    var n = _nodes[i] as AFlowNode;
                    if (n != null) {
                        for (int j = 0; j < n.NodeCount; ++j) {
                            if (n.GetNode(j) == node) {
                                n.RemoveNode(node);
                            }
                        }
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Creates connection between one node to another. The connection is one one-way and tries to prevent circular to prevent infinity loops.
        /// </summary>
        /// <param name="from">A node that is higher in hierarchy.</param>
        /// <param name="to">A node that is lower in hierarchy.</param>
        /// <returns>True if connection succeed. Otherwise false.</returns>
        public bool ConnectNodes(AFlowNode from, ANode to) {
            if (_nodes.Contains(from) && _nodes.Contains(to) && to != Root) {
                var n = to as AFlowNode;
                if (n != null) {
                    //check nodes recursive to prevent circular trees
                    if (IsConnected(from, n)) {
                        return false;
                    }
                }
                return from.AddNode(to);
            }
            return false;
        }

        /// <summary>
        /// Runs this tree behaviour.
        /// </summary>
        /// <param name="parameters">AiBlackboard with global parameters.</param>
        /// <param name="trees">Readonly list with all ai trees.</param>
        /// <param name="tasks">List of task scripts to bind with task nodes.</param>
        /// <returns>True if tree succeed. Otherwise false.</returns>
        public bool Run(AiBlackboard parameters, IList<AiTree> trees, List<TaskBinder> tasks) {
            return Root.Run(parameters, trees, tasks);
        }

        /// <summary>
        /// Runs debug this tree behaviour.
        /// </summary>
        /// <param name="parameters">AiBlackboard with global parameters.</param>
        /// <param name="trees">Readonly list with all ai trees.</param>
        /// <returns>True if debug run succeed. Otherwise false.</returns>
        public bool DebugRun(AiBlackboard parameters, IList<AiTree> trees) {
            var result = Root.DebugRun(parameters, trees);
            return result;
        }

        public List<string> GetTaskNodeKeys() {
            var keys = new List<string>();
            for (int i = 0; i < _nodes.Count; ++i) {
                var n = _nodes[i] as TaskNode;
                if (n != null) {
                    keys.Add(n.TaskKey);
                }
            }
            return keys;
        }

        /// <summary>
        /// Check recursive connections between nodes.
        /// </summary>
        /// <param name="from">node that should be higher in node tree</param>
        /// <param name="to">node that should be lower in node tree</param>
        /// <returns>True if there is connection. Otherwise false.</returns>
        private bool IsConnected(AFlowNode from, AFlowNode to) {
            for (int i = 0; i < to.NodeCount; ++i) {
                var n = to.GetNode(i) as AFlowNode;
                if (n != null) {
                    if (from == n || IsConnected(from, n)) {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
