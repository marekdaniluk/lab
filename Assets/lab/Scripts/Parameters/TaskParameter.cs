using UnityEngine;
using System.Collections.Generic;

namespace lab {
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

        public int Count {
            get { return _keys.Count; }
        }

        public string[] Keys {
            get { return _keys.ToArray(); }
        }

        public bool ContainsKey(string key) {
            return _keys.Contains(key);
        }

        public void AddKey(string key) {
            _keys.Add(key);
        }

        public void Remove(string key) {
            _keys.Remove(key);
        }
    }
}
