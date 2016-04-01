using UnityEngine;
using System.Collections.Generic;

namespace lab {
    /// <summary>
    /// Data asset with parameters and behavour trees.
    /// <para>AiBlackboard collects parameters in key-value manner. Typical blackboards are responsible only for that, but AiBlackboard is more similar to Unity's Animator. It handles not only parameters, but also bevahour trees.
    /// Single AiBehaviour has own parameters and can have multiple trees. If many objects have reference to same AiBehaviour, that means they all can run and change AiBehaviour's state.</para>
    /// </summary>
    [System.Serializable]
    public class AiBlackboard : ScriptableObject {

        [SerializeField]
        private ParameterContainer _parameters;
        [SerializeField]
        private List<AiTree> _trees = new List<AiTree>();

        /// <summary>
        /// Gets a readonly list of all behaviour trees assigned to this AiBlackboard.
        /// </summary>
        public IList<AiTree> Trees {
            get { return _trees.AsReadOnly(); }
        }

        /// <summary>
        /// Getter for int parameters.
        /// </summary>
        public IntParameter IntParameters {
            get { return _parameters.IntParameters; }
        }

        /// <summary>
        /// Getter for float parameters.
        /// </summary>
        public FloatParameter FloatParameters {
            get { return _parameters.FloatParameters; }
        }

        /// <summary>
        /// Getter for bool parameters.
        /// </summary>
        public BoolParameter BoolParameters {
            get { return _parameters.BoolParameters; }
        }

        /// <summary>
        /// Getter for string parameters.
        /// </summary>
        public StringParameter StringParameters {
            get { return _parameters.StringParameters; }
        }

        public bool Run(List<ATaskScript> tasks, int i = 0) {
            return _trees[i].Run(_parameters, tasks);
        }

        /// <summary>
        /// Add new behaviour tree to this blackboard.
        /// </summary>
        /// <param name="tree">New behaviour tree for current blackboard.</param>
        /// <returns>True if new tree was added. Otherwise false.</returns>
        public bool AddTree(AiTree tree) {
            if (_trees.Contains(tree)) {
                return false;
            }
            _trees.Add(tree);
            return true;
        }

        /// <summary>
        /// Remove behaviour tree from this blackboard.
        /// </summary>
        /// <param name="tree">Tree to remove from current blackboard.</param>
        /// <returns>True if tree was removed. Otherwise false.</returns>
        public bool RemoveTree(AiTree tree) {
            return _trees.Remove(tree);
        }
    }
}
