using UnityEngine;
using System.Collections.Generic;

namespace lab {
    /// <summary>
    /// Component to control ai behaviour.
    /// <para>AiController inherits from MonoBehaviour, so  this one should be attached to your game object. It is also responsible for binding tasks scripts with the logic (trees' tasks).
    /// Simply attach a AiBehaviour asset and tasks scripts. Use AiController to modificate AiBehaviour's parameters.</para>
    /// </summary>
    /// <example>
    /// This sample shows how to update parameters for current controller and run default behaviour tree.
    /// <code>
    ///using UnityEngine;
    ///using System.Collections;
    ///using lab;
    ///
    ///public class ExampleClass : MonoBehaviour {
    ///    public AiController controller;
    ///    void Start() {
    ///        controller.SetInt("money", 10);
    ///        if (!controller.Run()) {
    ///            Debug.Log("first tree failed.");
    ///        }
    ///    }
    ///}
    /// </code>
    /// </example>
    [DisallowMultipleComponent]
    public class AiController : MonoBehaviour {

        [SerializeField]
        private AiBehaviour _behaviour;
        [SerializeField]
        private List<ATaskScript> _tasks;

        private AiBehaviour _clonedBehaviour;

        /// <summary>
        /// Getter for AiBehaviour asset.
        /// </summary>
        public AiBehaviour Behaviour {
            get {
                if (_behaviour == null) {
                    return null;
                }
                if (_clonedBehaviour == null) {
                    _clonedBehaviour = (AiBehaviour)Instantiate(_behaviour);
                    _clonedBehaviour.name = _behaviour.name;
                }
                return _clonedBehaviour;
            }
        }

        /// <summary>
        /// Setter/Getter for task list.
        /// </summary>
        public List<ATaskScript> Tasks {
            get { return _tasks; }
            set { _tasks = value; }
        }

        /// <summary>
        /// Sets int parameters for ai behaviour.
        /// </summary>
        /// <param name="key">The name of the int parameters.</param>
        /// <param name="val">The new value for the int parameters.</param>
        public void SetInt(string key, int val) {
            Behaviour.IntParameters[key] = val;
        }

        /// <summary>
        /// Gets the int parameters for ai behaviour.
        /// </summary>
        /// <param name="key">The name of the int parameters.</param>
        /// <returns>The value for the int parameters.</returns>
        public int GetInt(string key) {
            return Behaviour.IntParameters[key];
        }

        /// <summary>
        /// Getter for all keys for int parameters.
        /// </summary>
        public IntParameter.KeyCollection IntKeys {
            get { return Behaviour.IntParameters.Keys; }
        }

        /// <summary>
        /// Sets float parameters for ai behaviour.
        /// </summary>
        /// <param name="key">The name of the float parameters.</param>
        /// <param name="val">The new value for the float parameters.</param>
        public void SetFloat(string key, float val) {
            Behaviour.FloatParameters[key] = val;
        }

        /// <summary>
        /// Gets the float parameters for ai behaviour.
        /// </summary>
        /// <param name="key">The name of the float parameters.</param>
        /// <returns>The value for the float parameters.</returns>
        public float GetFloat(string key) {
            return Behaviour.FloatParameters[key];
        }

        /// <summary>
        /// Getter for all keys for float parameters.
        /// </summary>
        public FloatParameter.KeyCollection FloatKeys {
            get { return Behaviour.FloatParameters.Keys; }
        }

        /// <summary>
        /// Sets bool parameters for ai behaviour.
        /// </summary>
        /// <param name="key">The name of the bool parameters.</param>
        /// <param name="val">The new value for the bool parameters.</param>
        public void SetBool(string key, bool val) {
            Behaviour.BoolParameters[key] = val;
        }

        /// <summary>
        /// Gets the bool parameters for ai behaviour.
        /// </summary>
        /// <param name="key">The name of the bool parameters.</param>
        /// <returns>The value for the bool parameters.</returns>
        public bool GetBool(string key) {
            return Behaviour.BoolParameters[key];
        }

        /// <summary>
        /// Getter for all keys for bool parameters.
        /// </summary>
        public BoolParameter.KeyCollection BoolKeys {
            get { return Behaviour.BoolParameters.Keys; }
        }

        /// <summary>
        /// Sets string parameters for ai behaviour.
        /// </summary>
        /// <param name="key">The name of the string parameters.</param>
        /// <param name="val">The new value for the string parameters.</param>
        public void SetString(string key, string val) {
            Behaviour.StringParameters[key] = val;
        }

        /// <summary>
        /// Gets the string parameters for ai behaviour.
        /// </summary>
        /// <param name="key">The name of the string parameters.</param>
        /// <returns>The value for the string parameters.</returns>
        public string GetString(string key) {
            return Behaviour.StringParameters[key];
        }

        /// <summary>
        /// Getter for all keys for string parameters.
        /// </summary>
        public StringParameter.KeyCollection StringKyes {
            get { return Behaviour.StringParameters.Keys; }
        }

        /// <summary>
        /// Run tree behaviour.
        /// </summary>
        /// <param name="i">Index of tree to run. Default value is 0.</param>
        /// <returns>True if tree succeed. Otherwise false.</returns>
        public bool Run(int i = 0) {
            return Behaviour.Run(Tasks, i);
        }

        /// <summary>
        /// Getter for a list of behaviour trees.
        /// </summary>
        public IList<AiTree> Trees {
            get { return Behaviour.Trees; }
        }
    }
}
