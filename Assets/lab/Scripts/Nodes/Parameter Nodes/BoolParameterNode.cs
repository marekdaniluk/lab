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
			switch (_condition) {
				case BoolCondition.Equal:
					if (Blackboard.BoolParameters[Key] == (DynamicValue ? Blackboard.BoolParameters[DynamicValueKey] : Value)) {
						return true;
					}
					break;
				case BoolCondition.NotEqual:
					if (Blackboard.BoolParameters[Key] != (DynamicValue ? Blackboard.BoolParameters[DynamicValueKey] : Value)) {
						return true;
					}
					break;
			}
			return false;
        }
#if UNITY_EDITOR
		public override bool DebugRun(int level, int nodeIndex) {
			var result = false;
			switch (_condition) {
			case BoolCondition.Equal:
				if (Blackboard.BoolParameters[Key] == (DynamicValue ? Blackboard.BoolParameters[DynamicValueKey] : Value)) {
					result = true;
				}
				break;
			case BoolCondition.NotEqual:
				if (Blackboard.BoolParameters[Key] != (DynamicValue ? Blackboard.BoolParameters[DynamicValueKey] : Value)) {
					result = true;
				}
				break;
			}
			Debug.Log(string.Format("{0}{1}. Bool Parameter Node. Result: <b><color={2}>{3}</color></b>", new string('\t', level), nodeIndex, result ? "green" : "red", result));
			OnDebugResult(this, result);
			return result;
        }
#endif
    }
}
