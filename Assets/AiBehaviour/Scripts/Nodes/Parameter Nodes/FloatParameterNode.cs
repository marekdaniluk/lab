using UnityEngine;

namespace AiBehaviour {
    [System.Serializable]
    public class FloatParameterNode : AParameterNode<float> {

        public enum FloatCondition {
            Greater,
            Less,
            Equal,
            NotEqual
        };

        [SerializeField]
        private FloatCondition _condition;

        public FloatCondition Condition {
            get { return _condition; }
            set { _condition = value; }
        }

        public override bool Run() {
            switch (_condition) {
                case FloatCondition.Greater: {
                    if (Blackboard.FloatParameters[Key] > Value) {
                        return true;
                    }
                    break;
                }
                case FloatCondition.Less: {
                    if (Blackboard.FloatParameters[Key] < Value) {
                        return true;
                    }
                    break;
                }
                case FloatCondition.Equal: {
                    if (Blackboard.FloatParameters[Key] == Value) {
                        return true;
                    }
                    break;
                }
                case FloatCondition.NotEqual: {
                    if (Blackboard.FloatParameters[Key] != Value) {
                        return true;
                    }
                    break;
                }
            }
            return false;
        }

        public override string ToString() {
            return string.Format("Is {0} {1} from {2}?", Blackboard.FloatParameters[Key], Condition.ToString(), Value);
        }
    }
}
