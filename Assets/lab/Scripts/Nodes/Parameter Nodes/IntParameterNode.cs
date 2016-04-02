using UnityEngine;
using System.Collections.Generic;

namespace lab {
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

        public override bool Run(Blackboard parameters, IList<AiTree> trees, List<ATaskScript> tasks) {
            switch (_condition) {
                case IntCondition.Greater:
                    if (parameters.IntParameters[Key] > (DynamicValue ? parameters.IntParameters[DynamicValueKey] : Value)) {
                        return true;
                    }
                    break;
                case IntCondition.Less:
                    if (parameters.IntParameters[Key] < (DynamicValue ? parameters.IntParameters[DynamicValueKey] : Value)) {
                        return true;
                    }
                    break;
                case IntCondition.Equal:
                    if (parameters.IntParameters[Key] == (DynamicValue ? parameters.IntParameters[DynamicValueKey] : Value)) {
                        return true;
                    }
                    break;
                case IntCondition.NotEqual:
                    if (parameters.IntParameters[Key] != (DynamicValue ? parameters.IntParameters[DynamicValueKey] : Value)) {
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
			case IntCondition.Greater:
				if (parameters.IntParameters[Key] > (DynamicValue ? parameters.IntParameters[DynamicValueKey] : Value)) {
					result = true;
				}
				break;
			case IntCondition.Less:
				if (parameters.IntParameters[Key] < (DynamicValue ? parameters.IntParameters[DynamicValueKey] : Value)) {
					result = true;
				}
				break;
			case IntCondition.Equal:
				if (parameters.IntParameters[Key] == (DynamicValue ? parameters.IntParameters[DynamicValueKey] : Value)) {
					result = true;
				}
				break;
			case IntCondition.NotEqual:
				if (parameters.IntParameters[Key] != (DynamicValue ? parameters.IntParameters[DynamicValueKey] : Value)) {
					result = true;
				}
				break;
			}
			Debug.Log(string.Format("{0}{1}. Int Parameter Node. Result: <b><color={2}>{3}</color></b>", new string('\t', level), nodeIndex, result ? "green" : "red", result));
			OnDebugResult(this, result);
			return result;
		}
		#endif
    }
}