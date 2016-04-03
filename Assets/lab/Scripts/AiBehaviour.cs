using UnityEngine;
using System.Collections.Generic;

namespace lab {
    /// <summary>
    /// Data asset with parameters and behaviour trees.
    /// <para>AiBehaviour collects parameters in key-value manner. AiBehaviour is similar to Unity's Animator. It handles not only parameters, but also bevahiour trees.
    /// Single AiBehaviour has a blackboard and can have multiple trees.</para>
    /// </summary>
    [System.Serializable]
    public class AiBehaviour : ScriptableObject {

        [SerializeField]
        private AiBlackboard _parameters;
        [SerializeField]
        private List<AiTree> _trees = new List<AiTree>();

        /// <summary>
        /// Gets a AiBlackboard for this AiBehaviour.
        /// </summary>
        public AiBlackboard Blackboard {
            get { return _parameters; }
        }

        /// <summary>
        /// Gets readonly list of all behaviour trees assigned to this AiBehaviour.
        /// </summary>
        public IList<AiTree> Trees {
            get { return _trees.AsReadOnly(); }
        }

        /// <summary>
        /// Adds new behaviour tree to this blackboard.
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
        /// Removes behaviour tree from this blackboard.
        /// </summary>
        /// <param name="tree">Tree to remove from current blackboard.</param>
        /// <returns>True if tree was removed. Otherwise false.</returns>
        public bool RemoveTree(AiTree tree) {
            return _trees.Remove(tree);
        }
    }
}
