using UnityEngine;
using System.Collections.Generic;

namespace lab {
    /// <summary>
    /// Parameter node that provides possibility for comparing AiBlackboard's float values.
    /// <para>FloatParameterNode can check if two values are equal or not equal to each other, first is greater or less to another.</para>
    /// </summary>
    [System.Serializable]
    public class FloatParameterNode : AParameterNode<float> {

        /// <summary>
        /// Enumeration for float parameters of possible conditions to choose to compare two values.
        /// </summary>
        public enum FloatCondition {
            Greater,
            Less,
            Equal,
            NotEqual
        };

        [SerializeField]
        private FloatCondition _condition;

        /// <summary>
        /// Sets/Gets condition to compare two float values.
        /// </summary>
        public FloatCondition Condition {
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
        public override bool Run(AiBlackboard parameters, IList<AiTree> trees, List<ATaskScript> tasks) {
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

        /// <summary>
        /// Runs debug this node.
        /// </summary>
        /// <param name="parameters">AiBlackboard with global parameters.</param>
        /// <param name="trees">Readonly list with all ai trees.</param>
        /// <param name="level">Level of how deep we are in this AiTree.</param>
        /// <param name="nodeIndex">Index of current node in parent's node. If this is root, nodeIndex is 0.</param>
        /// <returns>True if current conditions of comparition succeed. Otherwise false.</returns>
        public override bool DebugRun(AiBlackboard parameters, IList<AiTree> trees, int level, int nodeIndex) {
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
    }
}
