namespace lab {
    /// <summary>
    /// Abstract, base class for flow nodes.
    /// <para>AFlowNode is a type of ANode that composites other nodes. That means, only AFlowNode can be a root (first node) in AiTree.</para>
    /// </summary>
    [System.Serializable]
    public abstract class AFlowNode : ANode {

        /// <summary>
        /// Adds new node as a child.
        /// </summary>
        /// <param name="node">Node to be added as child.</param>
        /// <returns>True if adding succeed. Otherwise false.</returns>
        public abstract bool AddNode(ANode node);

        /// <summary>
        /// Removes child node.
        /// </summary>
        /// <param name="node">Child node to remove.</param>
        /// <returns>True if removing succeed. Otherwise false.</returns>
        public abstract bool RemoveNode(ANode node);

        /// <summary>
        /// Gets child node at index.
        /// </summary>
        /// <param name="i">Index of child node to get.</param>
        /// <returns>Child node under i index.</returns>
        public abstract ANode GetNode(int i);

        /// <summary>
        /// Gets count of child nodes.
        /// </summary>
        public abstract int NodeCount { get; }
    }
}
