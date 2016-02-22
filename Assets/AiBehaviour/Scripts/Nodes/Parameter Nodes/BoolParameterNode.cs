using UnityEngine;
using System.Collections.Generic;

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

        public override bool Run(List<ATaskScript> tasks) {
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
    }
}
