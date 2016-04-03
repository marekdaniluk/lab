using UnityEngine;
using System.Collections.Generic;

namespace lab {
    /// <summary>
    /// Behaviour tree with ai logic.
    /// <para></para>
    /// </summary>
    [System.Serializable]
    public class AiTree {

        [SerializeField]
        private ANode _root;
        [SerializeField]
        private List<ANode> _nodes = new List<ANode>();

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        public IList<ANode> Nodes {
            get { return _nodes.AsReadOnly(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool AddNode(ANode node) {
            if (_nodes.Contains(node)) {
                return false;
            }
            _nodes.Add(node);
            if (Root == null) {
                Root = node;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
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

        ///// <summary>
        ///// Run tree behaviour.
        ///// </summary>
        ///// <param name="tasks">List of tasks to bind with task nodes.</param>
        ///// <param name="i">Index of tree to run. Default value is 0.</param>
        ///// <returns>True if tree succeed. Otherwise false.</returns>
        //public bool Run(List<ATaskScript> tasks, int i = 0) {
        //    return _trees[i].Run(_parameters, Trees, tasks);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public bool Run(AiBlackboard parameters, IList<AiTree> trees, List<ATaskScript> tasks) {
            return Root.Run(parameters, trees, tasks);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="trees"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool DebugRun(AiBlackboard parameters, IList<AiTree> trees, int level) {
            var result = Root.DebugRun(parameters, trees, (level + 1), 0);
            level = Mathf.Clamp(level, 0, level);
            Debug.Log(string.Format("{0}<b>Tree debug run. Result: <color={1}>{2}</color></b>", new string('\t', level), result ? "green" : "red", result));
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private bool IsConnected(AFlowNode from, AFlowNode to) {
            for (int i = 0; i < to.NodeCount; ++i) {
                var n = to.GetNode(i) as AFlowNode;
                if (n != null) {
                    if (from == n) {
                        return true;
                    }
                    if (IsConnected(from, n)) {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
