using UnityEngine;
using System.Collections.Generic;

namespace lab {
    /// <summary>
    /// Parameter node that provides possibility for comparing AiBlackboard's int values.
    /// <para>IntParameterNode can check if two values are equal or not equal to each other, first is greater or less to another.</para>
    /// </summary>
    [System.Serializable]
    public class IntParameterNode : AParameterNode<int> {

        /// <summary>
        /// Enumeration for int parameters of possible conditions to choose to compare two values.
        /// </summary>
        public enum IntCondition {
            Greater,
            Less,
            Equal,
            NotEqual
        };

        [SerializeField]
        private IntCondition _condition;

        /// <summary>
        /// Sets/Gets condition to compare two int values.
        /// </summary>
        public IntCondition Condition {
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
    }
}