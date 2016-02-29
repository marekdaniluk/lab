using UnityEngine;
using System.Collections.Generic;

namespace lab {
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

        public override bool Run(List<ATaskScript> tasks) {
            switch (_condition) {
                case FloatCondition.Greater:
                    if (Blackboard.FloatParameters[Key] > (DynamicValue ? Blackboard.FloatParameters[DynamicValueKey] : Value)) {
                        return true;
                    }
                    break;
                case FloatCondition.Less:
                    if (Blackboard.FloatParameters[Key] < (DynamicValue ? Blackboard.FloatParameters[DynamicValueKey] : Value)) {
                        return true;
                    }
                    break;
                case FloatCondition.Equal:
                    if (Blackboard.FloatParameters[Key] == (DynamicValue ? Blackboard.FloatParameters[DynamicValueKey] : Value)) {
                        return true;
                    }
                    break;
                case FloatCondition.NotEqual:
                    if (Blackboard.FloatParameters[Key] != (DynamicValue ? Blackboard.FloatParameters[DynamicValueKey] : Value)) {
                        return true;
                    }
                    break;
            }
            return false;
        }
#if UNITY_EDITOR
        public override bool DebugRun(int level, int nodeIndex) {
            Debug.Log(string.Format("{0}{1}. Succeeder Node. Result: <b><color=green>true</color></b>", new string('\t', level), nodeIndex));
            return true;
        }
#endif
    }
}
