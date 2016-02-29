using UnityEngine;
using System.Collections.Generic;

namespace lab {
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

        public override bool Run(List<ATaskScript> tasks) {
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
#if UNITY_EDITOR
        public override bool DebugRun(int level, int nodeIndex) {
            Debug.Log(string.Format("{0}{1}. Succeeder Node. Result: <b><color=green>true</color></b>", new string('\t', level), nodeIndex));
            return true;
        }
#endif
    }
}
