using UnityEngine;

namespace AiBehaviour {
    [System.Serializable]
    public class BoolParameterNode : AParameterNode<bool> {

        public enum BoolCondition {
            Equal,
            NotEqual
        };

        [SerializeField]
        private BoolCondition _condition;

        public BoolCondition Condition {
            get { return _condition; }
            set { _condition = value; }
        }

        public override bool Run() {
            if (DynamicValue) {
                switch (_condition) {
                    case BoolCondition.Equal:
                        if (Blackboard.BoolParameters[Key].Equals(Blackboard.BoolParameters[DynamicValueKey])) {
                            return true;
                        }
                        break;
                    case BoolCondition.NotEqual:
                        if (!Blackboard.BoolParameters[Key].Equals(Blackboard.BoolParameters[DynamicValueKey])) {
                            return true;
                        }
                        break;
                }
            }
            return Blackboard.BoolParameters[Key] == Value;
        }

        public override string ToString() {
            return string.Format("Is {0} {1}?", Blackboard.BoolParameters[Key], (DynamicValue ? Blackboard.BoolParameters[DynamicValueKey] : Value));
        }
    }
}
