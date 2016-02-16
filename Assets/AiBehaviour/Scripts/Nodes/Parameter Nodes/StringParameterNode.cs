using UnityEngine;

namespace AiBehaviour {
    [System.Serializable]
    public class StringParameterNode : AParameterNode<string> {

        public enum StringCondition {
            Equal,
            NotEqual
        };

        [SerializeField]
        private StringCondition _condition;

        public StringCondition Condition {
            get { return _condition; }
            set { _condition = value; }
        }

        public override bool Run() {
            switch (_condition) {
                case StringCondition.Equal:
                    if (Blackboard.StringParameters[Key].Equals((DynamicValue ? Blackboard.StringParameters[DynamicValueKey] : Value))) {
                        return true;
                    }
                    break;
                case StringCondition.NotEqual:
                    if (!Blackboard.StringParameters[Key].Equals((DynamicValue ? Blackboard.StringParameters[DynamicValueKey] : Value))) {
                        return true;
                    }
                    break;
            }
            return false;
        }

        public override string ToString() {
            return string.Format("Is {0} {1} from {2}?", Blackboard.StringParameters[Key], Condition.ToString(), (DynamicValue ? Blackboard.StringParameters[DynamicValueKey] : Value));
        }
    }
}
