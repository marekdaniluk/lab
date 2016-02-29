using UnityEngine;

namespace lab {
    [System.Serializable]
    public abstract class ABlackboardNode : ANode {

        [SerializeField]
        private AiBlackboard _blackboard;

        public AiBlackboard Blackboard {
            get { return _blackboard; }
            set { _blackboard = value; }
        }
    }
}
