using UnityEngine;
using System.Collections.Generic;

namespace lab {
    /// <summary>
    /// Serializable task parameter that is used to map TaskNodes to ATaskScripts. This one is simple list of keys without values, not like other parameters.
    /// Other types of nodes are responsible for tree flow. Task nodes are representation of designed tasks, so they need to be paired with specific implementations.
    /// </summary>
    [System.Serializable]
    public class TaskParameter {

        [SerializeField, HideInInspector]
        private List<string> _keys = new List<string>();

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TaskParameter() {
            _keys = new List<string>();
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="asp">Serializable parameter to copy</param>
        public TaskParameter(TaskParameter asp) {
            _keys = new List<string>(asp._keys);
        }

        /// <summary>
        /// Total number of keys.
        /// </summary>
        public int Count {
            get { return _keys.Count; }
        }

        /// <summary>
        /// Array of keys.
        /// </summary>
        public string[] Keys {
            get { return _keys.ToArray(); }
        }

        /// <summary>
        /// Check if key contains.
        /// </summary>
        /// <param name="key">key to check if contains</param>
        /// <returns>True if key contains. Otherwise false.</returns>
        public bool ContainsKey(string key) {
            return _keys.Contains(key);
        }

        /// <summary>
        /// Add new key.
        /// </summary>
        /// <param name="key">key to add</param>
        public void AddKey(string key) {
            _keys.Add(key);
        }

        /// <summary>
        /// Remove key.
        /// </summary>
        /// <param name="key">key to remove</param>
        public void Remove(string key) {
            _keys.Remove(key);
        }
    }
}
