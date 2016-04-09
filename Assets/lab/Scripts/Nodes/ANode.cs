using UnityEngine;
using System.Collections.Generic;

namespace lab {
    /// <summary>
    /// Base abstract class for AiTree nodes.
    /// <para>Unity does not serialize interfaces, but it works with assets that inherits from ScriptableObjects. Every node is an asset that is part of AiBehaviour asset.
    /// Because of that, you cannot share nodes between behaviours. Node system uses Composite pattern, that means we have composites named AFlowNode and other types of nodes that are leafs in AiTree.</para>
    /// </summary>
    [System.Serializable]
    public abstract class ANode : ScriptableObject {

#if UNITY_EDITOR
        public delegate void DebugNodeHandler(ANode node, bool result);
        public DebugNodeHandler OnDebugResult = delegate { };

        [SerializeField, HideInInspector]
        public Vector2 Position;
#endif

        /// <summary>
        /// Runs this node.
        /// </summary>
        /// <param name="parameters">AiBlackboard with global parameters.</param>
        /// <param name="trees">Readonly list with all ai trees.</param>
        /// <param name="tasks">List of task scripts to bind with task nodes.</param>
        /// <returns>True if node succeed. Otherwise false.</returns>
        public abstract bool Run(AiBlackboard parameters, IList<AiTree> trees, List<ATaskScript> tasks);

        /// <summary>
        /// Runs debug this node.
        /// </summary>
        /// <param name="parameters">AiBlackboard with global parameters.</param>
        /// <param name="trees">Readonly list with all ai trees.</param>
        /// <param name="level">Level of how deep we are in this AiTree.</param>
        /// <param name="nodeIndex">Index of current node in parent's node. If this is root, nodeIndex is 0.</param>
        /// <returns>True if debug run succeed. Otherwise false.</returns>
        public abstract bool DebugRun(AiBlackboard parameters, IList<AiTree> trees, int level, int nodeIndex);
    }
}
