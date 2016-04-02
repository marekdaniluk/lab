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

        public override bool Run(ParameterContainer parameters, IList<AiTree> trees, List<ATaskScript> tasks) {
            switch (_condition) {
                case FloatCondition.Greater:
                    if (parameters.FloatParameters[Key] > (DynamicValue ? parameters.FloatParameters[DynamicValueKey] : Value)) {
                        return true;
                    }
                    break;
                case FloatCondition.Less:
                    if (parameters.FloatParameters[Key] < (DynamicValue ? parameters.FloatParameters[DynamicValueKey] : Value)) {
                        return true;
                    }
                    break;
                case FloatCondition.Equal:
                    if (parameters.FloatParameters[Key] == (DynamicValue ? parameters.FloatParameters[DynamicValueKey] : Value)) {
                        return true;
                    }
                    break;
                case FloatCondition.NotEqual:
                    if (parameters.FloatParameters[Key] != (DynamicValue ? parameters.FloatParameters[DynamicValueKey] : Value)) {
                        return true;
                    }
                    break;
            }
            return false;
		}
		#if UNITY_EDITOR
		public override bool DebugRun(ParameterContainer parameters, IList<AiTree> trees, int level, int nodeIndex) {
			var result = false;
			switch (_condition) {
			case FloatCondition.Greater:
				if (parameters.FloatParameters[Key] > (DynamicValue ? parameters.FloatParameters[DynamicValueKey] : Value)) {
					result = true;
				}
				break;
			case FloatCondition.Less:
				if (parameters.FloatParameters[Key] < (DynamicValue ? parameters.FloatParameters[DynamicValueKey] : Value)) {
					result = true;
				}
				break;
			case FloatCondition.Equal:
				if (parameters.FloatParameters[Key] == (DynamicValue ? parameters.FloatParameters[DynamicValueKey] : Value)) {
					result = true;
				}
				break;
			case FloatCondition.NotEqual:
				if (parameters.FloatParameters[Key] != (DynamicValue ? parameters.FloatParameters[DynamicValueKey] : Value)) {
					result = true;
				}
				break;
			}
			Debug.Log(string.Format("{0}{1}. Float Parameter Node. Result: <b><color={2}>{3}</color></b>", new string('\t', level), nodeIndex, result ? "green" : "red", result));
			OnDebugResult(this, result);
			return result;
		}
		#endif
    }
}
