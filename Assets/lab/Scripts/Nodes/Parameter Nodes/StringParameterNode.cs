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

        public override bool Run(Blackboard parameters, IList<AiTree> trees, List<ATaskScript> tasks) {
            switch (_condition) {
                case StringCondition.Equal:
                    if (parameters.StringParameters[Key].Equals((DynamicValue ? parameters.StringParameters[DynamicValueKey] : Value))) {
                        return true;
                    }
                    break;
                case StringCondition.NotEqual:
                    if (!parameters.StringParameters[Key].Equals((DynamicValue ? parameters.StringParameters[DynamicValueKey] : Value))) {
                        return true;
                    }
                    break;
            }
            return false;
		}
		#if UNITY_EDITOR
		public override bool DebugRun(Blackboard parameters, IList<AiTree> trees, int level, int nodeIndex) {
			var result = false;
			switch (_condition) {
				case StringCondition.Equal:
					if (parameters.StringParameters[Key] == (DynamicValue ? parameters.StringParameters[DynamicValueKey] : Value)) {
						result = true;
					}
					break;
				case StringCondition.NotEqual:
					if (parameters.StringParameters[Key] != (DynamicValue ? parameters.StringParameters[DynamicValueKey] : Value)) {
						result = true;
					}
					break;
			}
			Debug.Log(string.Format("{0}{1}. String Parameter Node. Result: <b><color={2}>{3}</color></b>", new string('\t', level), nodeIndex, result ? "green" : "red", result));
			OnDebugResult(this, result);
			return result;
		}
		#endif
    }
}
