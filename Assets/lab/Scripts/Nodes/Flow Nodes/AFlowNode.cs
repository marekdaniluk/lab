using UnityEngine;

namespace lab {
    [System.Serializable]
    public abstract class AFlowNode : ANode {

        public abstract bool AddNode(ANode node);

        public abstract bool RemoveNode(ANode node);

        public abstract ANode GetNode(int i);

        public abstract int NodeCount { get; }
    }
}
