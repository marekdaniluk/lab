using UnityEngine;
using System.Collections.Generic;

namespace lab {
    /// <summary>
    /// Generic abstract class for dictionary serialization.
    /// <para>Unity cannot serialize dictionaries, so ASerializableParameter implements ISerializationCallbackReceiver. This gives possibility of custom serialization.
    /// However, this solution does not work with generic types, so we need to inherit from ASerializableParameter.</para>
    /// </summary>
    /// <typeparam name="TKey">Key type</typeparam>
    /// <typeparam name="TValue">Value type</typeparam>
    [System.Serializable]
    public abstract class ASerializableParameter<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver {

        [SerializeField, HideInInspector]
        private List<TKey> _keys = new List<TKey>();
        [SerializeField, HideInInspector]
        private List<TValue> _values = new List<TValue>();

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ASerializableParameter() : base() {
            _keys = new List<TKey>();
            _values = new List<TValue>();
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="asp">Serializable parameter to copy</param>
        public ASerializableParameter(ASerializableParameter<TKey, TValue> asp) : base(asp) {
            _keys = new List<TKey>(asp._keys);
            _values = new List<TValue>(asp._values);
        }

        /// <summary>
        /// Method to receive a callback before Unity serializes your object.
        /// </summary>
        public void OnBeforeSerialize() {
            _keys.Clear();
            _values.Clear();
            foreach (KeyValuePair<TKey, TValue> pair in this) {
                _keys.Add(pair.Key);
                _values.Add(pair.Value);
            }
        }

        /// <summary>
        /// Method to receive a callback after Unity de-serializes your object.
        /// </summary>
        public void OnAfterDeserialize() {
            this.Clear();
            if (_keys.Count != _values.Count) {
                throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable.", _keys.Count, _values.Count));
            }
            for (int i = 0; i < _keys.Count; i++) {
                this.Add(_keys[i], _values[i]);
            }
        }
    }
}
