using UnityEngine;
using System.Collections.Generic;

namespace lab {
    /// <summary>
    /// Parameter node that provides possibility for comparing AiBlackboard's string values.
    /// <para>StringParameterNode can check if two values are equal or not equal to each other.</para>
    /// </summary>
    [System.Serializable]
    public class StringParameterNode : AParameterNode<string> {

        /// <summary>
        /// Enumeration for string parameters of possible conditions to choose to compare two values.
        /// </summary>
        public enum StringCondition {
            Equal,
            NotEqual
        };

        [SerializeField]
        private StringCondition _condition;

        /// <summary>
        /// Sets/Gets condition to compare two string values.
        /// </summary>
        public StringCondition Condition {
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
    }
}
