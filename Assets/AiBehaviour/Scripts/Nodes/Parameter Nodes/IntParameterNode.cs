using UnityEngine;

namespace AiBehaviour {
    [System.Serializable]
    public class IntParameterNode : AParameterNode<int> {

        public enum IntCondition {
            Greater,
            Less,
            Equal,
            NotEqual
        };

        [SerializeField]
        private IntCondition _condition;

        public IntCondition Condition {
            get { return _condition; }
            set { _condition = value; }
        }

        public override bool Run() {
            switch (_condition) {
                case IntCondition.Greater:
                    if (Blackboard.IntParameters[Key] > (DynamicValue ? Blackboard.IntParameters[DynamicValueKey] : Value)) {
                        return true;
                    }
                    break;
                case IntCondition.Less:
                    if (Blackboard.IntParameters[Key] < (DynamicValue ? Blackboard.IntParameters[DynamicValueKey] : Value)) {
                        return true;
                    }
                    break;
                case IntCondition.Equal:
                    if (Blackboard.IntParameters[Key] == (DynamicValue ? Blackboard.IntParameters[DynamicValueKey] : Value)) {
                        return true;
                    }
                    break;
                case IntCondition.NotEqual:
                    if (Blackboard.IntParameters[Key] != (DynamicValue ? Blackboard.IntParameters[DynamicValueKey] : Value)) {
                        return true;
                    }
                    break;
            }
            return false;
        }
    }
}