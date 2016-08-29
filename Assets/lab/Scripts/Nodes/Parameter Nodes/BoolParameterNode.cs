using UnityEngine;
using System.Collections.Generic;

namespace lab {
    /// <summary>
    /// Parameter node that provides possibility for comparing AiBlackboard's bool values.
    /// <para>BoolParameterNode can check if two values are equal or not equal to each other.</para>
    /// </summary>
    [System.Serializable]
    public class BoolParameterNode : AParameterNode<bool> {

        /// <summary>
        /// Enumeration for bool parameters of possible conditions to choose to compare two values.
        /// </summary>
        public enum BoolCondition {
            Equal,
            NotEqual
        };

        [SerializeField]
        private BoolCondition _condition;

        /// <summary>
        /// Sets/Gets condition to compare two bool values.
        /// </summary>
        public BoolCondition Condition {
            get { return _condition; }
            set { _condition = value; }
        }

        /// <summary>
        /// Runs this node.
        /// </summary>
        /// <param name="parameters">AiBlackboard with global parameters.</param>
        /// <param name="trees">Readonly list with all ai trees.</param>
        /// <param name="tasks">List of task scripts to bind with.</param>
        /// <returns>True if current conditions of comparition succeed. Otherwise false.</returns>
        public override bool Run(AiBlackboard parameters, IList<AiTree> trees, List<TaskBinder> tasks) {
            switch (_condition) {
                case BoolCondition.Equal:
                    if (parameters.BoolParameters[Key] == (DynamicValue ? parameters.BoolParameters[DynamicValueKey] : Value)) {
                        return true;
                    }
                    break;
                case BoolCondition.NotEqual:
                    if (parameters.BoolParameters[Key] != (DynamicValue ? parameters.BoolParameters[DynamicValueKey] : Value)) {
                        return true;
                    }
                    break;
            }
            return false;
        }

        /// <summary>
        /// Runs debug this node.
        /// </summary>
        /// <param name="parameters">AiBlackboard with global parameters.</param>
        /// <param name="trees">Readonly list with all ai trees.</param>
        /// <returns>True if current conditions of comparition succeed. Otherwise false.</returns>
        public override bool DebugRun(AiBlackboard parameters, IList<AiTree> trees) {
            var result = false;
            switch (_condition) {
                case BoolCondition.Equal:
                    if (parameters.BoolParameters[Key] == (DynamicValue ? parameters.BoolParameters[DynamicValueKey] : Value)) {
                        result = true;
                    }
                    break;
                case BoolCondition.NotEqual:
                    if (parameters.BoolParameters[Key] != (DynamicValue ? parameters.BoolParameters[DynamicValueKey] : Value)) {
                        result = true;
                    }
                    break;
            }
            OnDebugResult(this, result);
            return result;
        }

        public override string ToString() {
            return string.IsNullOrEmpty(Key) ? GetType().Name : Key;
        }
    }
}
