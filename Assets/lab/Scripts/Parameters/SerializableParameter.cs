using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SerializableParameter<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver {

    [SerializeField]
    [HideInInspector]
    private List<TKey> _keys = new List<TKey>();
    [SerializeField]
    [HideInInspector]
    private List<TValue> _values = new List<TValue>();

    public void OnBeforeSerialize() {
        _keys.Clear();
        _values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this) {
            _keys.Add(pair.Key);
            _values.Add(pair.Value);
        }
    }

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
