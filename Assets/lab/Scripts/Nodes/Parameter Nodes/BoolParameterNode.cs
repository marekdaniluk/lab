using UnityEngine;
using System.Collections.Generic;

namespace lab {
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
#if UNITY_EDITOR
        public override bool DebugRun(int level, int nodeIndex) {
            Debug.Log(string.Format("{0}{1}. Succeeder Node. Result: <b><color=green>true</color></b>", new string('\t', level), nodeIndex));
            return true;
        }
#endif
    }
}
