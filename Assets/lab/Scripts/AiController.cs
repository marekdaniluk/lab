using UnityEngine;
using System.Collections.Generic;

namespace lab {
    /// <summary>
    /// Component to control ai behaviour.
    /// <para>Mainly it is a wrapper for a blackboard object. It is also responsible for binding tasks with task nodes.</para>
    /// </summary>
    [DisallowMultipleComponent]
    public class AiController : MonoBehaviour {

        /// <summary>
        /// Gets/Sets global information for ai behaviour.
        /// </summary>
        [SerializeField]
        private AiBlackboard _blackboard;
        /// <summary>
        /// List of tasks to bind with task nodes.
        /// </summary>
        [SerializeField]
        private List<ATaskScript> _tasks;

        /// <summary>
        /// Setter/Getter for blackboard.
        /// </summary>
        public AiBlackboard Blackboard {
            get { return _blackboard; }
            set { _blackboard = value; }
        }

        /// <summary>
        /// Setter/Getter for task list.
        /// </summary>
        public List<ATaskScript> Tasks {
            get { return _tasks; }
            set { _tasks = value; }
        }

        /// <summary>
        /// Sets int global information for ai behaviour.
        /// </summary>
        /// <param name="key">The name of the int global information.</param>
        /// <param name="val">The new value for the int global information.</param>
        public void SetInt(string key, int val) {
            Blackboard.IntParameters[key] = val;
        }

        /// <summary>
        /// Gets the int global information for ai behaviour.
        /// </summary>
        /// <param name="key">The name of the int global information.</param>
        /// <returns>The value for the int global information.</returns>
        public int GetInt(string key) {
            return Blackboard.IntParameters[key];
        }

        /// <summary>
        /// Getter for all keys for int global information.
        /// </summary>
        public IntParameter.KeyCollection IntKeys {
            get { return Blackboard.IntParameters.Keys; }
        }

        /// <summary>
        /// Sets float global information for ai behaviour.
        /// </summary>
        /// <param name="key">The name of the float global information.</param>
        /// <param name="val">The new value for the float global information.</param>
        public void SetFloat(string key, float val) {
            Blackboard.FloatParameters[key] = val;
        }

        /// <summary>
        /// Gets the float global information for ai behaviour.
        /// </summary>
        /// <param name="key">The name of the float global information.</param>
        /// <returns>The value for the float global information.</returns>
        public float GetFloat(string key) {
            return Blackboard.FloatParameters[key];
        }

        /// <summary>
        /// Getter for all keys for float global information.
        /// </summary>
        public FloatParameter.KeyCollection FloatKeys {
            get { return Blackboard.FloatParameters.Keys; }
        }

        /// <summary>
        /// Sets bool global information for ai behaviour.
        /// </summary>
        /// <param name="key">The name of the bool global information.</param>
        /// <param name="val">The new value for the bool global information.</param>
        public void SetBool(string key, bool val) {
            Blackboard.BoolParameters[key] = val;
        }

        /// <summary>
        /// Gets the bool global information for ai behaviour.
        /// </summary>
        /// <param name="key">The name of the bool global information.</param>
        /// <returns>The value for the bool global information.</returns>
        public bool GetBool(string key) {
            return Blackboard.BoolParameters[key];
        }

        /// <summary>
        /// Getter for all keys for bool global information.
        /// </summary>
        public BoolParameter.KeyCollection BoolKeys {
            get { return Blackboard.BoolParameters.Keys; }
        }

        /// <summary>
        /// Sets string global information for ai behaviour.
        /// </summary>
        /// <param name="key">The name of the string global information.</param>
        /// <param name="val">The new value for the string global information.</param>
        public void SetString(string key, string val) {
            Blackboard.StringParameters[key] = val;
        }

        /// <summary>
        /// Gets the string global information for ai behaviour.
        /// </summary>
        /// <param name="key">The name of the string global information.</param>
        /// <returns>The value for the string global information.</returns>
        public string GetString(string key) {
            return Blackboard.StringParameters[key];
        }

        /// <summary>
        /// Getter for all keys for string global information.
        /// </summary>
        public StringParameter.KeyCollection StringKyes {
            get { return Blackboard.StringParameters.Keys; }
        }

        /// <summary>
        /// Run tree behaviour.
        /// </summary>
        /// <param name="i">Index of tree to run. Default value is 0.</param>
        /// <returns>True if tree succeed. Otherwise false.</returns>
        public bool Run(int i = 0) {
            return Blackboard.Run(Tasks, i);
        }

        /// <summary>
        /// Getter for a list of behaviour trees.
        /// </summary>
        public IList<AiTree> Trees {
            get { return Blackboard.Trees; }
        }
    }
}
